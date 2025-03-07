using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MifareCardProg
{
    public partial class MainMifareProg : Form
    {
        public int retCode,
            hContext,
            hCard,
            Protocol;

        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen,
            RecvLen,
            nBytesRet,
            reqType,
            Aprotocol,
            dwProtocol,
            cbPciLength;
        public ModWinsCard.SCARD_READERSTATE RdrState;
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest;
        private Bitmap originalImage;
        private string loadedFilePath;
        int[] availableBlocks = { 4, 5, 6, 8, 9, 10, 12, 13, 14 }; // Hindari sector trailer

        private static readonly int[] SECTOR_TRAILERS =
        {
            3,
            7,
            11,
            15,
            19,
            23,
            27,
            31,
            35,
            39,
            43,
            47,
            51,
            55,
            59,
            63,
            67,
            71,
            75,
            79,
            83,
            87,
            91,
            95,
            99,
            103,
            107,
            111,
            115,
            119,
            123,
            127,
            143,
            159,
            175,
            191,
            207,
            223,
            239,
            255,
        };

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
            tbHextoStr.Text = "";
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

            if (SendBuff.Length < 263 || RecvBuff.Length < 263)
            {
                throw new InvalidOperationException("Buffer sizes are incorrect.");
            }

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

        private void rbNonVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Enabled = true;
        }

        private void rbVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Enabled = false;
            tMemAdd.Text = "";
        }

        private void bLoadKey_Click(object sender, EventArgs e)
        {
            byte tmpLong;

            if (!(rbNonVolMem.Checked) & !(rbVolMem.Checked))
            {
                rbNonVolMem.Focus();
                return;
            }

            if (rbNonVolMem.Checked)
            {
                if (
                    tMemAdd.Text == ""
                    | !byte.TryParse(
                        tMemAdd.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tMemAdd.Focus();
                    tMemAdd.Text = "";
                    return;
                }

                if (byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber) > 31)
                {
                    tMemAdd.Text = "1F";
                    return;
                }
            }

            if (
                tKey1.Text == ""
                | !byte.TryParse(
                    tKey1.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey1.Focus();
                tKey1.Text = "";
                return;
            }

            if (
                tKey2.Text == ""
                | !byte.TryParse(
                    tKey2.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey2.Focus();
                tKey2.Text = "";
                return;
            }

            if (
                tKey3.Text == ""
                | !byte.TryParse(
                    tKey3.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey3.Focus();
                tKey3.Text = "";
                return;
            }

            if (
                tKey4.Text == ""
                | !byte.TryParse(
                    tKey4.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey4.Focus();
                tKey4.Text = "";
                return;
            }

            if (
                tKey5.Text == ""
                | !byte.TryParse(
                    tKey5.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey5.Focus();
                tKey5.Text = "";
                return;
            }

            if (
                tKey6.Text == ""
                | !byte.TryParse(
                    tKey6.Text,
                    System.Globalization.NumberStyles.HexNumber,
                    null,
                    out tmpLong
                )
            )
            {
                tKey6.Focus();
                tKey6.Text = "";
                return;
            }

            ClearBuffers();

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x82;
            if (rbNonVolMem.Checked)
            {
                SendBuff[2] = 0x20;
                SendBuff[3] = byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber);
            }
            else
            {
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x20;
            }

            SendBuff[4] = 0x06;
            SendBuff[5] = byte.Parse(tKey1.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[6] = byte.Parse(tKey2.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[7] = byte.Parse(tKey3.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[8] = byte.Parse(tKey4.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[9] = byte.Parse(tKey5.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[10] = byte.Parse(tKey6.Text, System.Globalization.NumberStyles.HexNumber);
            SendLen = 11;
            RecvLen = 2;

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

        private void bAuth_Click(object sender, EventArgs e)
        {
            int tempInt;
            byte tmpLong;

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
                if (
                    tKeyIn1.Text == ""
                    | !byte.TryParse(
                        tKeyIn1.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tKeyIn1.Focus();
                    tKeyIn1.Text = "";
                    return;
                }

                if (
                    tKeyIn2.Text == ""
                    | !byte.TryParse(
                        tKeyIn2.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tKeyIn2.Focus();
                    tKeyIn2.Text = "";
                    return;
                }

                if (
                    tKeyIn3.Text == ""
                    | !byte.TryParse(
                        tKeyIn3.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tKeyIn3.Focus();
                    tKeyIn3.Text = "";
                    return;
                }

                if (
                    tKeyIn4.Text == ""
                    | !byte.TryParse(
                        tKeyIn4.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tKeyIn4.Focus();
                    tKeyIn4.Text = "";
                    return;
                }

                if (
                    tKeyIn5.Text == ""
                    | !byte.TryParse(
                        tKeyIn5.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
                {
                    tKeyIn5.Focus();
                    tKeyIn5.Text = "";
                    return;
                }

                if (
                    tKeyIn6.Text == ""
                    | !byte.TryParse(
                        tKeyIn6.Text,
                        System.Globalization.NumberStyles.HexNumber,
                        null,
                        out tmpLong
                    )
                )
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
                    if (
                        tKeyAdd.Text == ""
                        | !byte.TryParse(
                            tKeyAdd.Text,
                            System.Globalization.NumberStyles.HexNumber,
                            null,
                            out tmpLong
                        )
                    )
                    {
                        tKeyAdd.Focus();
                        tKeyAdd.Text = "";
                        return;
                    }

                    if (
                        byte.Parse(tKeyAdd.Text, System.Globalization.NumberStyles.HexNumber) > 0x1F
                    )
                    {
                        tKeyAdd.Text = "1F";
                        return;
                    }
                }
            }

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            if (rbSource1.Checked == true)
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0x82;
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x20;
                SendBuff[4] = 0x06;
                SendBuff[5] = byte.Parse(tKeyIn1.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[6] = byte.Parse(tKeyIn2.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[7] = byte.Parse(tKeyIn3.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[8] = byte.Parse(tKeyIn4.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[9] = byte.Parse(tKeyIn5.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[10] = byte.Parse(
                    tKeyIn6.Text,
                    System.Globalization.NumberStyles.HexNumber
                );

                SendLen = 0x0B;
                RecvLen = 0x02;
                retCode = SendAPDUandDisplay(0);

                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    return;
                }

                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0x86;
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x00;
                SendBuff[4] = 0x05;
                SendBuff[5] = 0x01;
                SendBuff[6] = 0x00;
                SendBuff[7] = (byte)int.Parse(tBlkNo.Text);
                if (rbKType1.Checked == true)
                {
                    SendBuff[8] = 0x60;
                }
                else
                {
                    SendBuff[8] = 0x61;
                }

                SendBuff[9] = 0x20;
            }
            else
            {
                ClearBuffers();
                SendBuff[0] = 0xFF;
                SendBuff[1] = 0x86;
                SendBuff[2] = 0x00;
                SendBuff[3] = 0x00;
                SendBuff[4] = 0x05;
                SendBuff[5] = 0x01;
                SendBuff[6] = 0x00;
                SendBuff[7] = (byte)int.Parse(tBlkNo.Text);
                if (rbKType2.Checked == true)
                {
                    SendBuff[8] = 0x61;
                }
                else
                {
                    SendBuff[8] = 0x60;
                }

                if (rbSource2.Checked == true)
                {
                    SendBuff[9] = 0x20;
                }
                else
                {
                    SendBuff[9] = (byte)int.Parse(tKeyAdd.Text);
                }
            }

            SendLen = 0x0A;
            RecvLen = 0x02;
            retCode = SendAPDUandDisplay(0);

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

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD7;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);
            SendBuff[4] = 0x05;
            SendBuff[5] = 0x00;
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);
            SendBuff[9] = (byte)(Amount & 0xFF);
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

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD7;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);
            SendBuff[4] = 0x05;
            SendBuff[5] = 0x01;
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);
            SendBuff[9] = (byte)(Amount & 0xFF);
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02;
            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
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

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD7;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);
            SendBuff[4] = 0x05;
            SendBuff[5] = 0x02;
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF);
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF);
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF);
            SendBuff[9] = (byte)(Amount & 0xFF);
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
            if (int.Parse(tValBlk.Text) > 319)
            {
                tValBlk.Text = "319";
                return;
            }

            tValAmt.Text = "";
            tValSrc.Text = "";
            tValTar.Text = "";

            ClearBuffers();

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xB1;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tValBlk.Text);
            SendBuff[4] = 0x00;
            SendLen = 0x05;
            RecvLen = 0x06;
            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            Amount = RecvBuff[3];
            Amount = Amount + (RecvBuff[2] * 256);
            Amount = Amount + (RecvBuff[1] * 256 * 256);
            Amount = Amount + (RecvBuff[0] * 256 * 256 * 256);
            tValAmt.Text = Amount.ToString();
        }

        private void bValRes_Click(object sender, EventArgs e)
        {
            int tempInt;

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

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD7;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tValSrc.Text);
            SendBuff[4] = 0x02;
            SendBuff[5] = 0x03;
            SendBuff[6] = (byte)int.Parse(tValTar.Text);
            SendLen = 0x07;
            RecvLen = 0x02;
            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bHexRead_Click(object sender, EventArgs e)
        {
            string tmpStr;

            tbHextoStr.Text = "";

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

            tmpStr = "";

            tmpStr = ByteArrayToString(RecvBuff.Take(RecvLen - 2).ToArray());

            tbHextoStr.Text = tmpStr.ToUpper();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable
                .Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        private void bHexUpd_Click(object sender, EventArgs e)
        {
            int tempInt;

            if (tBinBlk.Text == "" || !int.TryParse(tBinBlk.Text, out tempInt))
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

            if (tBinLen.Text == "" || !int.TryParse(tBinLen.Text, out tempInt))
            {
                tBinLen.Focus();
                tBinLen.Text = "";
                return;
            }

            if (tbHextoStr.Text == "")
            {
                tbHextoStr.Focus();
                return;
            }

            byte[] byteArray = StringToByteArray(tbHextoStr.Text);
            if (byteArray.Length != int.Parse(tBinLen.Text))
                ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD6;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text);
            SendBuff[4] = (byte)int.Parse(tBinLen.Text);
            Array.Copy(byteArray, 0, SendBuff, 5, byteArray.Length);

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x00;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bReadAll_Click(object sender, EventArgs e)
        {
            int[] sectorSizes =
            {
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                4,
                16,
                16,
                16,
                16,
                16,
                16,
                16,
                16,
            };
            int totalSectors = sectorSizes.Length;
            int blockIndex = 0;
            int bytesPerBlock = 16;

            DataTable dt = new DataTable();
            dt.Columns.Add("Sektor", typeof(int));
            dt.Columns.Add("Blok", typeof(int));
            dt.Columns.Add("Data", typeof(string));

            try
            {
                for (int sector = 0; sector < totalSectors; sector++)
                {
                    int blocksInSector = sectorSizes[sector];
                    int trailerBlock = blockIndex + blocksInSector - 1;

                    byte authKey = rbKType2.Checked ? (byte)0x61 : (byte)0x60;

                    byte keySource = rbSource2.Checked ? (byte)0x20 : (byte)int.Parse(tKeyAdd.Text);

                    ClearBuffers();

                    SendBuff[0] = 0xFF;
                    SendBuff[1] = 0x86;
                    SendBuff[2] = 0x00;
                    SendBuff[3] = 0x00;
                    SendBuff[4] = 0x05;
                    SendBuff[5] = 0x01;
                    SendBuff[6] = 0x00;
                    SendBuff[7] = (byte)trailerBlock;
                    SendBuff[8] = authKey;
                    SendBuff[9] = keySource;

                    SendLen = 10;
                    RecvLen = 2;

                    if (SendAPDUandDisplay(0) != ModWinsCard.SCARD_S_SUCCESS)
                    {
                        MessageBox.Show(
                            $"Autentikasi gagal di sektor {sector} menggunakan {(authKey == 0x60 ? "Key A" : "Key B")}!",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    for (int i = 0; i < blocksInSector; i++)
                    {
                        int currentBlock = blockIndex + i;

                        ClearBuffers();

                        SendBuff[0] = 0xFF;
                        SendBuff[1] = 0xB0;
                        SendBuff[2] = 0x00;
                        SendBuff[3] = (byte)currentBlock;
                        SendBuff[4] = (byte)bytesPerBlock;

                        SendLen = 5;
                        RecvLen = bytesPerBlock + 2;

                        if (SendAPDUandDisplay(2) != ModWinsCard.SCARD_S_SUCCESS)
                        {
                            MessageBox.Show(
                                $"Gagal membaca blok {currentBlock}!",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return;
                        }

                        string blockData = ByteArrayToString(RecvBuff.Take(RecvLen - 2).ToArray())
                            .ToUpper();
                        dt.Rows.Add(sector, currentBlock, blockData);
                    }

                    blockIndex += blocksInSector;
                }

                dReadAll.DataSource = dt;
                dReadAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                MessageBox.Show(
                    "Pembacaan kartu selesai!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Terjadi kesalahan: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void bInit_Click(object sender, EventArgs e)
        {
            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            retCode = ModWinsCard.SCardEstablishContext(
                ModWinsCard.SCARD_SCOPE_USER,
                0,
                0,
                ref hContext
            );

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(1, retCode, "");
                return;
            }

            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(1, retCode, "");
                return;
            }

            EnableButtons();
            byte[] ReadersList = new byte[pcchReaders];
            retCode = ModWinsCard.SCardListReaders(
                this.hContext,
                null,
                ReadersList,
                ref pcchReaders
            );

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

            while (ReadersList[indx] != 0)
            {
                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx];
                    indx = indx + 1;
                }

                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;
            }

            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 0;
            }

            indx = 1;

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
            if (connActive)
            {
                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            }

            retCode = ModWinsCard.SCardConnect(
                hContext,
                cbReader.Text,
                ModWinsCard.SCARD_SHARE_SHARED,
                1 | 2,
                ref hCard,
                ref Protocol
            );

            if (retCode == ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(0, 0, "Successful connection to " + cbReader.Text);
            }
            else
            {
                displayOut(
                    0,
                    0,
                    "The smart card has been removed, so that further communication is not possible."
                );
            }

            connActive = true;
            gbLoadKeys.Enabled = true;
            gbAuth.Enabled = true;
            gbBinOps.Enabled = true;
            gbValBlk.Enabled = true;
            rbSource1.Checked = true;
            rbKType1.Checked = true;
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
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);
        }

        private int SendAPDUandDisplay(int reqType)
        {
            int indx;
            string tmpStr;

            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);
            }

            displayOut(2, 0, tmpStr);
            retCode = ModWinsCard.SCardTransmit(
                hCard,
                ref pioSendRequest,
                ref SendBuff[0],
                SendLen,
                ref pioSendRequest,
                ref RecvBuff[0],
                ref RecvLen
            );

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

        private void btnGetUID_Click(object sender, EventArgs e)
        {
            ClearBuffers();

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xCA;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x00;
            SendLen = 5;
            RecvLen = 10;
            retCode = SendAPDUandDisplay(2);
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        //profile card
        private List<byte[]> SplitData()
        {
            string hexData = EncodeProfileData();

            hexData = hexData.Replace(" ", "");

            byte[] byteArray = Enumerable
                .Range(0, hexData.Length / 2)
                .Select(i => Convert.ToByte(hexData.Substring(i * 2, 2), 16))
                .ToArray();

            int chunkSize = 16;
            List<byte[]> splitDataList = new List<byte[]>();
            for (int i = 0; i < byteArray.Length; i += chunkSize)
            {
                byte[] splitDataReturn = byteArray.Skip(i).Take(chunkSize).ToArray();
                splitDataList.Add(splitDataReturn);
                Debug.WriteLine(BitConverter.ToString(splitDataReturn).Replace("-", " "));
            }
            return splitDataList;
        }

        private void lblAddPhoto_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter =
                    "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png",
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = ofd.FileName;
                originalImage = new Bitmap(loadedFilePath);

                ProfilePict.SizeMode = PictureBoxSizeMode.Zoom;
                ProfilePict.Image = originalImage;

                UpdateLabelVisibility();
            }
        }

        private void ProfilePict_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter =
                    "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png",
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loadedFilePath = ofd.FileName;
                originalImage = new Bitmap(loadedFilePath);

                ProfilePict.SizeMode = PictureBoxSizeMode.Zoom;
                ProfilePict.Image = originalImage;

                UpdateLabelVisibility();
            }
        }

        private void UpdateLabelVisibility()
        {
            if (ProfilePict.Image != null)
            {
                lblAddPhoto.Visible = false;
            }
            else if (ProfilePict.Image == null)
            {
                lblAddPhoto.Visible = true;
            }
            else
            {
                lblAddPhoto.Visible = true;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            TxtAddress.Text = "";
            TxtBirthDate.Text = "";
            TxtGender.Text = "";
            TxtName.Text = "";
            TxtNumber.Text = "";
            ProfilePict.Image = null;
            UpdateLabelVisibility();
        }

        private string EncodeProfileData()
        {
            string name = TxtName.Text.Trim();
            string dob = TxtBirthDate.Text.Trim();
            string gender = TxtGender.Text.Trim();
            string address = TxtAddress.Text.Trim();
            string phone = TxtNumber.Text.Trim();

            byte[]? compressedImage = null;
            if (ProfilePict.Image != null)
            {
                compressedImage = CompressImage(ProfilePict.Image, 40, 60, 80);
            }
            string hexImage =
                compressedImage != null ? ConvertToHexWithHeader("PIC", compressedImage) : "PIC00";
            string hexName = ConvertToHexWithHeader("NME", name);
            string hexDOB = ConvertToHexWithHeader("DTE", dob);
            string hexGender = ConvertToHexWithHeader("GDR", gender);
            string hexAddress = ConvertToHexWithHeader("ADR", address);
            string hexPhone = ConvertToHexWithHeader("NUM", phone);

            return $"{hexImage} {hexName} {hexDOB} {hexGender} {hexAddress} {hexPhone}";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Please make sure your information is correct!",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                EncodeProfileData();
                SplitData();

                //MessageBox.Show(
                //    "Uploaded successfully!",
                //    "Success",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                //);
            }
            else { }
        }

        private static string ConvertToHexWithHeader(string header, byte[] data)
        {
            string hexHeader = BitConverter
                .ToString(Encoding.ASCII.GetBytes(header))
                .Replace("-", "");
            string hexData = BitConverter.ToString(data).Replace("-", "");
            return $"{hexHeader} {hexData}";
        }

        private static string ConvertToHexWithHeader(string header, string data)
        {
            string hexHeader = BitConverter
                .ToString(Encoding.ASCII.GetBytes(header))
                .Replace("-", "");
            string hexData = BitConverter.ToString(Encoding.ASCII.GetBytes(data)).Replace("-", "");
            return $"{hexHeader} {hexData}";
        }

        private static byte[] CompressImage(
            System.Drawing.Image image,
            int width,
            int height,
            long quality
        )
        {
            using Bitmap resizedImage = new Bitmap(image, new Size(width, height));
            using MemoryStream ms = new MemoryStream();
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                quality
            );
            resizedImage.Save(ms, jpgEncoder, encoderParameters);
            return ms.ToArray();
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void WriteBlock(int block, byte[] data)
        {
            if (data.Length > 16)
            {
                Debug.WriteLine("Data terlalu besar untuk satu block!");
                return;
            }

            SendBuff[0] = 0xFF;
            SendBuff[1] = 0xD6;
            SendBuff[2] = 0x00;
            SendBuff[3] = (byte)block;
            SendBuff[4] = (byte)data.Length;

            Array.Copy(data, 0, SendBuff, 5, data.Length);

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x00;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                Debug.WriteLine($"Gagal menulis ke block {block}");
            }
            else
            {
                Debug.WriteLine($"Berhasil menulis ke block {block}");
            }
        }

        private void WriteProfileData()
        {
            List<byte[]> splitData = SplitData();
            int block = 4;

            foreach (var data in splitData)
            {
                while (SECTOR_TRAILERS.Contains(block))
                {
                    block++;
                }

                WriteBlock(block, data);
                block++;
            }
        }
    }
}
