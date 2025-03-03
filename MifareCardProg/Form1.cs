/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   transact with Mifare 1K/4K cards using ACR128
'  
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 18, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'=========================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MifareCardProg
{
    public partial class MainMifareProg : Form
    {

        public int retCode, hContext, hCard, Protocol;
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, nBytesRet, reqType, Aprotocol, dwProtocol, cbPciLength;
        public ModWinsCard.SCARD_READERSTATE RdrState;
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest;

        public MainMifareProg()
        {
            InitializeComponent();
        }

        private void MainMifareProg_Load(object sender, EventArgs e)
        {
            InitMenu();

        }

        private void InitMenu()
        {

            connActive = false;
            cbReader.Items.Clear();
            cbReader.Text = "";
            mMsg.Items.Clear();
            displayOut(0, 0, "Program ready");
            bConnect.Enabled = false;
            bInit.Enabled = true;
            bReset.Enabled = false;
            rbNonVolMem.Checked = false;
            rbVolMem.Checked = false;
            tMemAdd.Text = "";
            tKey1.Text = "";
            tKey2.Text = "";
            tKey3.Text = "";
            tKey4.Text = "";
            tKey5.Text = "";
            tKey6.Text = "";
            gbLoadKeys.Enabled = false;
            tBlkNo.Text = "";
            tKeyAdd.Text = "";
            tKeyIn1.Text = "";
            tKeyIn2.Text = "";
            tKeyIn3.Text = "";
            tKeyIn4.Text = "";
            tKeyIn5.Text = "";
            tKeyIn6.Text = "";
            gbAuth.Enabled = false;
            tBinBlk.Text = "";
            tBinLen.Text = "";
            tBinData.Text = "";
            gbBinOps.Enabled = false;
            tValAmt.Text = "";
            tValBlk.Text = "";
            tValSrc.Text = "";
            tValTar.Text = "";
            gbValBlk.Enabled = false;
        }

        private void ClearBuffers()
        {

            long indx;

            for (indx = 0; indx <= 262; indx++)
            {

                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;

            }

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bReset.Enabled = true;
            bClear.Enabled = true;

        }
   
        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    break;
                case 1:
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    break;
                case 2:
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    PrintText = ">" + PrintText;
                    break;

            }
            mMsg.Items.Add(PrintText);
            mMsg.ForeColor = Color.Black;
            mMsg.Focus();

        }


        private void bInit_Click(object sender, EventArgs e)
        {
            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            // 1. Establish Context
            retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;

            }

            // 2. List PC/SC card readers installed in the system

            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;
            }

            EnableButtons();

            byte[] ReadersList = new byte[pcchReaders];

            // Fill reader list
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                mMsg.Items.Add("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retCode));
                mMsg.SelectedIndex = mMsg.Items.Count - 1;
                return;
            }
            else
            {
                displayOut(0, 0, " ");
            }

            rName = "";
            indx = 0;

            // Convert reader buffer to string
            while (ReadersList[indx] != 0)
            {

                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx];
                    indx = indx + 1;
                }

                // Add reader name to list
                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;

            }

            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 1;

            }
            indx = 1;

            // Look for ACR128 PICC and make it the default reader in the combobox

            for (indx = 1; indx <= cbReader.Items.Count - 1; indx++)
            {

                cbReader.SelectedIndex = indx;

                if (cbReader.Text == "ACS ACR128U PICC Interface 0") 
                {
                    cbReader.SelectedIndex = 1;
                    return;

                }
               
            }
                return; 
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            // Connect to selected reader using hContext handle and obtain hCard handle
            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            // Shared Connection
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref Protocol);

            if (retCode == ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(0,0,"Successful connection to " + cbReader.Text);
            }

            else
            {

                displayOut(0, 0, "The smart card has been removed, so that further communication is not possible.");

            }


            connActive = true;
            gbLoadKeys.Enabled = true;
            gbAuth.Enabled = true;
            gbBinOps.Enabled = true;
            gbValBlk.Enabled = true;
            
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            mMsg.Items.Clear();
        }

        private void bReset_Click(object sender, EventArgs e)
        {

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            retCode = ModWinsCard.SCardReleaseContext(hCard);

            InitMenu();
        }

        private void bQuit_Click(object sender, EventArgs e)
        {

            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);
        
        
        }

        private void bLoadKey_Click(object sender, EventArgs e)
        {
            byte tmpLong;

            // Check for valid inputs
            if ((!(rbNonVolMem.Checked) & !(rbVolMem.Checked)))
            {

                rbNonVolMem.Focus();
                return;

            }

            if (rbNonVolMem.Checked)
            {

                if (tMemAdd.Text == "" | !byte.TryParse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tMemAdd.Focus();
                    tMemAdd.Text = "";
                    return; 

                }

                if  (byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber) > 31)
                {

                    tMemAdd.Text = "1F";
                    return;

                }

            }

            if (tKey1.Text == "" | !byte.TryParse(tKey1.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey1.Focus();
                tKey1.Text = "";
                return; 

            }

            if (tKey2.Text == "" | !byte.TryParse(tKey2.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey2.Focus();
                tKey2.Text = "";
                return; 
            }

            if (tKey3.Text == "" | !byte.TryParse(tKey3.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey3.Focus();
                tKey3.Text = "";
                return;

            }

            if (tKey4.Text == "" | !byte.TryParse(tKey4.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey4.Focus();
                tKey4.Text = "";
                return; 
            }

            if (tKey5.Text == "" | !byte.TryParse(tKey5.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey5.Focus();
                tKey5.Text = "";
                return;

            }

            if (tKey6.Text == "" | !byte.TryParse(tKey6.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {

                tKey6.Focus();
                tKey6.Text = "";
                return; 
            }

            ClearBuffers();
            SendBuff[0] = 0xFF;                      // CLA
            SendBuff[1] = 0x82;                      // INS
            

            if (rbNonVolMem.Checked)
            {
                SendBuff[2] = 0x20;                 // P1 : Non volatile memory

                SendBuff[3] = byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber);   // P2 : Memory location
                                                                                                      
            }

            else
            {

                SendBuff[2] = 0x00;                 // P1 : Volatile memory
                SendBuff[3] = 0x20;                 // P2 : Session Key        
                

            }
            SendBuff[4] = 0x06;                     // P3

            SendBuff[5] = byte.Parse(tKey1.Text, System.Globalization.NumberStyles.HexNumber);        // Key 1 value
            SendBuff[6] = byte.Parse(tKey2.Text, System.Globalization.NumberStyles.HexNumber);        // Key 2 value
            SendBuff[7] = byte.Parse(tKey3.Text, System.Globalization.NumberStyles.HexNumber);        // Key 3 value
            SendBuff[8] = byte.Parse(tKey4.Text, System.Globalization.NumberStyles.HexNumber);        // Key 4 value
            SendBuff[9] = byte.Parse(tKey5.Text, System.Globalization.NumberStyles.HexNumber);        // Key 5 value
            SendBuff[10] = byte.Parse(tKey6.Text, System.Globalization.NumberStyles.HexNumber);       // Key 6 value
            
            SendLen = 11;
            RecvLen = 2;

            retCode = SendAPDUandDisplay(0);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        
        }

        private int SendAPDUandDisplay(int reqType)
        {

            int indx;
            string tmpStr;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            // Display Apdu In
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);
            retCode = ModWinsCard.SCardTransmit(hCard, ref pioSendRequest, ref SendBuff[0], SendLen, ref pioSendRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
                return retCode;


            }

            else
            {

                tmpStr = "";
                switch (reqType)
                {

                    case 0:
                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                        }


                        if ((tmpStr).Trim() != "90 00")
                        {

                            displayOut(4, 0, "Return bytes are not acceptable.");

                        }

                        break;

                    case 1:

                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

                        }


                        if (tmpStr.Trim() != "90 00")
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                        }

                        else
                        {

                            tmpStr = "ATR : ";
                            for (indx = 0; indx <= (RecvLen - 3); indx++)
                            {

                                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                            }

                        }

                        break;

                    case 2:

                        for (indx = 0; indx <= (RecvLen - 1); indx++)
                        {

                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

                        }

                        break;

                }

                displayOut(3, 0, tmpStr.Trim());

            }
            return retCode;


        }

        private void rbNonVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Enabled = true;
        }

        private void rbVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Text = "";
            tMemAdd.Enabled = false;
        }

        private void bAuth_Click(object sender, EventArgs e)
        {
            int tempInt;
            byte tmpLong;

            // Validate input
            if (tBlkNo.Text == "" | !int.TryParse(tBlkNo.Text, out tempInt))
            {

                tBlkNo.Focus();
                tBlkNo.Text = "";
                return;

            }

            if (int.Parse(tBlkNo.Text) > 319)
            {

                tBlkNo.Text = "319";

            }

            if (rbSource1.Checked == true)
            {

                if (tKeyIn1.Text == "" | !byte.TryParse(tKeyIn1.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn1.Focus();
                    tKeyIn1.Text = "";
                    return; 

                }

                if (tKeyIn2.Text == "" | !byte.TryParse(tKeyIn2.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn2.Focus();
                    tKeyIn2.Text = "";
                    return; 

                }

                if (tKeyIn3.Text == "" | !byte.TryParse(tKeyIn3.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn3.Focus();
                    tKeyIn3.Text = "";
                    return; 

                }

                if (tKeyIn4.Text == "" | !byte.TryParse(tKeyIn4.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn4.Focus();
                    tKeyIn4.Text = "";
                    return; 

                }

                if (tKeyIn5.Text == "" | !byte.TryParse(tKeyIn5.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn5.Focus();
                    tKeyIn5.Text = "";
                    return; 

                }

                if (tKeyIn6.Text == "" | !byte.TryParse(tKeyIn6.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                {

                    tKeyIn6.Focus();
                    tKeyIn6.Text = "";
                    return;

                }
            }

            else
            {

                if (rbSource3.Checked == true)
                {

                    if (tKeyAdd.Text == "" | !byte.TryParse(tKeyAdd.Text, System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
                    {

                        tKeyAdd.Focus();
                        tKeyAdd.Text = "";
                        return; 

                    }

                    if (byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber) > 31)
                    {

                        tKeyAdd.Text = "&H1F";

                    }

                }

            }

            ClearBuffers();
            SendBuff[0] = 0xFF;                                                 // CLA
            SendBuff[1] = 0x00;                                                 // P1 : Same for all source type
           

            if (rbSource1.Checked == true)
            {

                SendBuff[1] = 0x88;                                             // INS : for manual key input
                SendBuff[3] = (byte)int.Parse(tBlkNo.Text);                     // P2  : Sector No. for manual key input

               
                if (rbKType1.Checked == true)
                {

                    SendBuff[4] = 0x60;                                         // P3  : Key A for manual key input
                   
                }
                else
                {
                    SendBuff[4] = 0x61;                                         // P3  : Key B for manual key input        
                   

                }

                SendBuff[5] = byte.Parse(tKeyIn1.Text, System.Globalization.NumberStyles.HexNumber);        // Key 1 value
                SendBuff[6] = byte.Parse(tKeyIn2.Text, System.Globalization.NumberStyles.HexNumber);        // Key 2 value
                SendBuff[7] = byte.Parse(tKeyIn3.Text, System.Globalization.NumberStyles.HexNumber);        // Key 3 value
                SendBuff[8] = byte.Parse(tKeyIn4.Text, System.Globalization.NumberStyles.HexNumber);        // Key 4 value
                SendBuff[9] = byte.Parse(tKeyIn5.Text, System.Globalization.NumberStyles.HexNumber);        // Key 5 value
                SendBuff[10] = byte.Parse(tKeyIn6.Text, System.Globalization.NumberStyles.HexNumber);       // Key 6 value
            
            }

            else
            {

                SendBuff[1] = 0x86;                                          // INS : for stored key input            
                SendBuff[3] = 0x00;                                          // P2  : for stored key input
                SendBuff[4] = 0x05;                                          // P3  : for stored key input
                SendBuff[5] = 0x01;                                          // Byte 1 : Version no.       
                SendBuff[6] = 0x00;                                          // Byte 2
                SendBuff[7] = (byte)int.Parse(tBlkNo.Text);                  // Byte 3 : Sector No. for stored key input
                

                if (rbKType1.Checked == true)
                {

                    SendBuff[8] = 0x60;                                     // Byte 4 : Key A for stored key input
                    
                }

                else
                {

                    SendBuff[8] = 0x61;                                     // Byte 4 : Key B for stored key input
                                
                }

                if (rbSource2.Checked == true)
                {

                    SendBuff[9] = 0x20;                                     // Byte 5 : Session key for volatile memory
                    
                }

                else
                {

                    SendBuff[9] = byte.Parse(tKeyAdd.Text, System.Globalization.NumberStyles.HexNumber);          // Byte 5 : Key No. for non-volatile memory
                  

                }

            }

            if (rbSource1.Checked == true)
            {

                SendLen = 0x0B;
            }

            else
            {

                SendLen = 0x0A;
            }

            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(0);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        }

        private void rbSource1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSource1.Checked == true)
            {

                tBlkNo.Enabled = true;
                tKeyAdd.Enabled = false;
                tKeyIn1.Enabled = true;
                tKeyIn2.Enabled = true;
                tKeyIn3.Enabled = true;
                tKeyIn4.Enabled = true;
                tKeyIn5.Enabled = true;
                tKeyIn6.Enabled = true;
                return; 

            }
        }

        private void rbSource2_CheckedChanged(object sender, EventArgs e)
        {

            if (rbSource2.Checked == true)
            {

                tBlkNo.Enabled = true;
                tKeyAdd.Enabled = false;
                tKeyIn1.Enabled = false;
                tKeyIn2.Enabled = false;
                tKeyIn3.Enabled = false;
                tKeyIn4.Enabled = false;
                tKeyIn5.Enabled = false;
                tKeyIn6.Enabled = false;
                return; 
            }
        
        }

        private void rbSource3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSource3.Checked == true)
            {

                tBlkNo.Enabled = true;
                tKeyAdd.Enabled = true;
                tKeyIn1.Enabled = false;
                tKeyIn2.Enabled = false;
                tKeyIn3.Enabled = false;
                tKeyIn4.Enabled = false;
                tKeyIn5.Enabled = false;
                tKeyIn6.Enabled = false;
                return; 

            }
        
        }

        private void bBinRead_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;

            // Validate Inputs
            tBinData.Text = "";

            if (tBinBlk.Text == "")
            {

                tBinBlk.Focus();
                return; 

            }

            if (int.Parse(tBinBlk.Text) > 319)
            {

                tBinBlk.Text = "319";
                return; 
            }

            if (tBinLen.Text == "")
            {

                tBinLen.Focus();
                return;

            }

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xB0;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text);
            SendBuff[4] = (byte)int.Parse(tBinLen.Text);

            SendLen = 5;
            RecvLen = SendBuff[4] + 2;

            retCode = SendAPDUandDisplay(2);


            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

            // Display data in text format

            tmpStr = "";


            for (indx = 0; indx <= RecvLen - 1; indx++)
            {

                tmpStr = tmpStr + Convert.ToChar(RecvBuff[indx]);

            }

            tBinData.Text = tmpStr;
        
        }

        private void bBinUpd_Click(object sender, EventArgs e)
        {
           
            string tmpStr;
            int indx, tempInt;

            if (tBinBlk.Text == "" | !int.TryParse(tBinBlk.Text, out tempInt))
            {

                tBinBlk.Focus();
                tBinBlk.Text = "";
                return; 

            }

            if (int.Parse(tBinBlk.Text) > 319)
            {

                tBinBlk.Text = "319";
                return; 

            }

            if (tBinLen.Text == "" | !int.TryParse(tBinLen.Text, out tempInt))
            {

                tBinLen.Focus();
                tBinLen.Text = "";
                return; 

            }


            if (tBinData.Text == "")
            {

                tBinData.Focus();
                return;

            }

            tmpStr = tBinData.Text;
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD6;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text);            // P2 : Starting Block No.
            SendBuff[4] = (byte)int.Parse(tBinLen.Text);            // P3 : Data length

            for (indx = 0; indx <= (tmpStr).Length - 1; indx++)
            {

                SendBuff[indx + 5] = (byte)tmpStr[indx];

            }
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        }

        private void bValStor_Click(object sender, EventArgs e)
        {

            long Amount;
            int tempInt;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return; 

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return; 

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return; 

            }

            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return; 

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x00;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        }

        private void bValInc_Click(object sender, EventArgs e)
        {

            long Amount;
            int tempInt;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return;

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return;

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }


            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x01;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }
        }

        private void bValRead_Click(object sender, EventArgs e)
        {
             
            long Amount;

            if (int.Parse(tValBlk.Text) > 319) {
            
            tValBlk.Text = "319";
            return; 
            
            }
        
            tValAmt.Text = "";
            tValSrc.Text = "";
            tValTar.Text = "";
        
            ClearBuffers();
            SendBuff[0] = 0xFF;                                      // CLA     
            SendBuff[1] = 0xB1;                                      // INS
            SendBuff[2] = 0x00;                                      // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);             // P2 : Block No.
            SendBuff[4] = 0x00;                                      // Le
        
            SendLen = 0x05;
            RecvLen = 0x06;
        
            retCode = SendAPDUandDisplay(2);
        
            if (retCode != ModWinsCard.SCARD_S_SUCCESS) {
            
                return; 
            
            }
        
            Amount = RecvBuff[3];
            Amount = Amount + (RecvBuff[2] * 256);
            Amount = Amount + (RecvBuff[1] * 256 * 256);
            Amount = Amount + (RecvBuff[0] * 256 * 256 * 256);
            tValAmt.Text =(Amount).ToString();
        
        }

        private void bValDec_Click(object sender, EventArgs e)
        {
            long Amount;
            int tempInt;

            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {

                tValAmt.Focus();
                tValAmt.Text = "";
                return;

            }

            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {

                tValAmt.Text = "4294967295";
                tValAmt.Focus();
                return;

            }

            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValBlk.Focus();
                tValBlk.Text = "";
                return;

            }


            if (int.Parse(tValBlk.Text) > 319)
            {

                tValBlk.Text = "319";
                return;

            }

            tValSrc.Text = "";
            tValTar.Text = "";

            Amount = Convert.ToInt64(tValAmt.Text);
            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);            // P2 : Block No.
            SendBuff[4] = 0x05;                                     // Lc : Data length
            SendBuff[5] = 0x02;                                     // VB_OP Value
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);            // Amount MSByte
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);            // Amount middle byte         
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);             // Amount middle byte
            SendBuff[9] = (byte)(Amount & 0xFF);                    // Amount LSByte

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return;

            }
        }

        private void bValRes_Click(object sender, EventArgs e)
        {
            
            int tempInt;
            
            // Validate inputs
            if (tValSrc.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValSrc.Focus();
                tValSrc.Text = "";
                return; 

            }

            if (tValTar.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {

                tValTar.Focus();
                tValTar.Text = "";
                return; 

            }

            if (int.Parse(tValSrc.Text) > 319)
            {

                tValSrc.Text = "319";
                return; 

            }

            if (int.Parse(tValTar.Text) > 319)
            {

                tValTar.Text = "319";
                return; 

            }

            tValAmt.Text = "";
            tValBlk.Text = "";

            ClearBuffers();
            SendBuff[0] = 0xFF;                                     // CLA
            SendBuff[1] = 0xD7;                                     // INS
            SendBuff[2] = 0x00;                                     // P1
            SendBuff[3] = (byte)int.Parse(tValSrc.Text);            // P2 : Source Block No.
            SendBuff[4] = 0x02;                                     // Lc
            SendBuff[5] = 0x03;                                     // Data In Byte 1
            SendBuff[6] = (byte)int.Parse(tValTar.Text);            // P2 : Target Block No.

            SendLen = 0x07;
            RecvLen = 0x02;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }
        
        }

        private void tBinLen_TextChanged(object sender, EventArgs e)
        {

            byte numbyte;

            if (byte.TryParse(tBinLen.Text, out numbyte))
                tBinData.MaxLength = numbyte;

        }

        
    }
}