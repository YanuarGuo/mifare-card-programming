using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MifareCardProg
{
    public partial class MainMifareProg : Form
    {
        private DataGridView dgvSectorTrailer;
        private DataGridView dgvDataBlock2;
        private DataGridView dgvDataBlock1;
        private DataGridView dgvDataBlock0;
        private List<string[]> checkedDataBlock0;
        private List<string[]> checkedDataBlock1;
        private List<string[]> checkedDataBlock3;
        private List<string[]> checkedSectorTrailer;
        private BindingList<DataBlockCondition> accessConditionsBlock0;
        private BindingList<DataBlockCondition> accessConditionsBlock1;
        private BindingList<DataBlockCondition> accessConditionsBlock2;
        private BindingList<SectorTrailerCondition> accessConditionsST;
        public int retCode,
            hContext,
            hCard,
            Protocol;
        public int[] bitfieldTable = { 0x0, 0x2, 0x1, 0x3, 0x4, 0x6, 0x5, 0x7 };
        public bool connActive = false;
        public bool autoDet;
        public byte[] SendBuff = new byte[263]; // Buffer untuk mengirim data ke kartu
        public byte[] RecvBuff = new byte[263]; // Buffer untuk menerima data dari kartu
        public int SendLen,
            RecvLen,
            nBytesRet,
            reqType,
            Aprotocol,
            dwProtocol,
            cbPciLength;
        public ModWinsCard.SCARD_READERSTATE RdrState; // Status pembaca kartu
        public ModWinsCard.SCARD_IO_REQUEST pioSendRequest; // Struktur untuk request ke kartu
        public TextBox[] accessBitTextBoxes;

        public MainMifareProg()
        {
            bool[,] test = new bool[8, 3];

            //array access condition
            //0
            test[3, 1] = false; // C10
            test[7, 2] = false; // C20
            test[3, 2] = false; // C30

            test[3, 0] = !(test[7, 2]); // -C20
            test[7, 0] = !(test[3, 1]); // -C10
            test[7, 1] = !(test[3, 2]); // -C30

            //1
            test[2, 1] = true; //C11
            test[2, 2] = true; //C31
            test[6, 2] = true; //C21

            test[6, 0] = !(test[2, 1]); // -C11
            test[6, 1] = !(test[0, 2]); // -C31
            test[2, 0] = !(test[6, 2]); // -C21

            //2
            test[1, 1] = true; // C12
            test[1, 2] = true; // C32
            test[6, 2] = true; // C22

            test[6, 0] = !(test[1, 1]); // -C12
            test[6, 1] = !(test[1, 2]); // -C32
            test[1, 0] = !(test[6, 2]); // -C22

            // sector trailer
            test[0, 1] = true; // C13
            test[4, 2] = true; // C33
            test[0, 2] = true; // C20

            test[0, 0] = !(test[4, 2]); // -C23
            test[4, 0] = !(test[0, 2]); // -C13
            test[4, 1] = !(test[0, 2]); // -C33

            InitializeComponent();
            InitializeTabControl();
            accessBitTextBoxes = new TextBox[] { tAB1, tAB2, tAB3, tAB4 };
            checkedDataBlock0 = GetCheckedRows(dgvDataBlock0);
            checkedDataBlock1 = GetCheckedRows(dgvDataBlock1);
            checkedDataBlock3 = GetCheckedRows(dgvDataBlock2);
            checkedSectorTrailer = GetCheckedRows(dgvSectorTrailer);
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

            // Ensure SendBuff and RecvBuff are correctly sized
            if (SendBuff.Length < 263 || RecvBuff.Length < 263)
            {
                throw new InvalidOperationException("Buffer sizes are incorrect.");
            }

            for (indx = 0; indx <= 262; indx++)
            {
                RecvBuff[indx] = 0; // Clear receive buffer
                SendBuff[indx] = 0; // Clear send buffer
            }
        }

        private void EnableButtons()
        {
            bInit.Enabled = false; // Nonaktifkan tombol "Initialize"
            bConnect.Enabled = true; // Aktifkan tombol "Connect"
            bReset.Enabled = true; // Aktifkan tombol "Reset"
            bClear.Enabled = true; // Aktifkan tombol "Clear"
        }

        private void displayOut(int errType, int retVal, string PrintText)
        {
            switch (errType)
            {
                case 0: // Tidak ada error, tampilkan pesan biasa
                    break;
                case 1: // Tampilkan pesan error berdasarkan kode error
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    break;
                case 2: // Format pesan dengan "<" di awal
                    PrintText = "<" + PrintText;
                    break;
                case 3: // Format pesan dengan ">" di awal
                    PrintText = ">" + PrintText;
                    break;
            }
            mMsg.Items.Add(PrintText); // Tambahkan pesan ke daftar pesan
            mMsg.ForeColor = Color.Black; // Set warna teks menjadi hitam
            mMsg.Focus(); // Fokus pada daftar pesan
        }

        private void rbNonVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Enabled = true; // Mengaktifkan input tMemAdd jika Non-Volatile Memory dipilih
        }

        private void rbVolMem_CheckedChanged(object sender, EventArgs e)
        {
            tMemAdd.Enabled = false; // Menonaktifkan input tMemAdd jika Volatile Memory dipilih
            tMemAdd.Text = ""; // Mengosongkan nilai tMemAdd
        }

        private void bLoadKey_Click(object sender, EventArgs e)
        {
            byte tmpLong;

            // Cek apakah salah satu radio button sudah dipilih
            if (!(rbNonVolMem.Checked) & !(rbVolMem.Checked))
            {
                rbNonVolMem.Focus(); // Fokus ke rbNonVolMem jika belum ada yang dipilih
                return; // Keluar dari fungsi
            }

            // Jika Non-Volatile Memory dipilih, lakukan validasi input tMemAdd
            if (rbNonVolMem.Checked)
            {
                // Cek apakah tMemAdd kosong atau bukan angka dalam format hex
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
                    tMemAdd.Focus(); // Fokus ke tMemAdd jika input salah
                    tMemAdd.Text = ""; // Kosongkan input
                    return; // Keluar dari fungsi
                }

                // Cek apakah nilai dalam tMemAdd lebih dari 31 (1F dalam hex)
                if (byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber) > 31)
                {
                    tMemAdd.Text = "1F"; // Jika lebih dari 31, set nilai ke "1F"
                    return; // Keluar dari fungsi
                }
            }

            // Validasi semua input kunci (tKey1 - tKey6)
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

            // Membersihkan buffer sebelum mengisi perintah
            ClearBuffers();

            // Mengisi buffer untuk mengirim perintah APDU
            SendBuff[0] = 0xFF; // CLA (Class Byte)
            SendBuff[1] = 0x82; // INS (Instruction Byte)

            // Menentukan jenis memori yang digunakan
            if (rbNonVolMem.Checked)
            {
                SendBuff[2] = 0x20; // P1 : Non-Volatile Memory
                SendBuff[3] = byte.Parse(tMemAdd.Text, System.Globalization.NumberStyles.HexNumber); // P2 : Alamat memori
            }
            else
            {
                SendBuff[2] = 0x00; // P1 : Volatile Memory
                SendBuff[3] = 0x20; // P2 : Session Key
            }

            SendBuff[4] = 0x06; // P3 : Panjang data (6 byte)

            // Mengisi buffer dengan nilai kunci dari inputan pengguna
            SendBuff[5] = byte.Parse(tKey1.Text, System.Globalization.NumberStyles.HexNumber); // Key 1
            SendBuff[6] = byte.Parse(tKey2.Text, System.Globalization.NumberStyles.HexNumber); // Key 2
            SendBuff[7] = byte.Parse(tKey3.Text, System.Globalization.NumberStyles.HexNumber); // Key 3
            SendBuff[8] = byte.Parse(tKey4.Text, System.Globalization.NumberStyles.HexNumber); // Key 4
            SendBuff[9] = byte.Parse(tKey5.Text, System.Globalization.NumberStyles.HexNumber); // Key 5
            SendBuff[10] = byte.Parse(tKey6.Text, System.Globalization.NumberStyles.HexNumber); // Key 6

            // Menentukan panjang buffer untuk dikirim dan diterima
            SendLen = 11;
            RecvLen = 2;

            // Mengirim perintah APDU dan menangani hasilnya
            retCode = SendAPDUandDisplay(0);

            // Jika terjadi kesalahan dalam pengiriman APDU, keluar dari fungsi
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

            // Validasi input: pastikan tBlkNo tidak kosong dan bisa dikonversi ke integer
            if (tBlkNo.Text == "" | !int.TryParse(tBlkNo.Text, out tempInt))
            {
                tBlkNo.Focus();
                tBlkNo.Text = "";
                return;
            }

            // Batasi nilai tBlkNo maksimal 319
            if (int.Parse(tBlkNo.Text) > 319)
            {
                tBlkNo.Text = "319";
            }

            // Jika rbSource1 dipilih, validasi semua input kunci (tKeyIn1 - tKeyIn6)
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
                // Jika rbSource3 dipilih, validasi tKeyAdd
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

                    // Batasi nilai maksimal tKeyAdd ke 0x1F
                    if (
                        byte.Parse(tKeyAdd.Text, System.Globalization.NumberStyles.HexNumber) > 0x1F
                    )
                    {
                        tKeyAdd.Text = "1F";
                        return;
                    }
                }
            }

            // Bersihkan buffer sebelum mengirim perintah
            ClearBuffers();
            SendBuff[0] = 0xFF; // CLA
            SendBuff[1] = 0x00; // P1: Sama untuk semua sumber

            if (rbSource1.Checked == true)
            {
                // Menggunakan memori volatil untuk otentikasi
                ClearBuffers();
                SendBuff[0] = 0xFF; // CLS
                SendBuff[1] = 0x82; // INS
                SendBuff[2] = 0x00; // P1: Memori volatil
                SendBuff[3] = 0x20; // P2: Kunci sesi
                SendBuff[4] = 0x06; // P3: Panjang kunci (6 byte)

                // Menyimpan nilai kunci 1-6 ke dalam buffer
                SendBuff[5] = byte.Parse(tKeyIn1.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[6] = byte.Parse(tKeyIn2.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[7] = byte.Parse(tKeyIn3.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[8] = byte.Parse(tKeyIn4.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[9] = byte.Parse(tKeyIn5.Text, System.Globalization.NumberStyles.HexNumber);
                SendBuff[10] = byte.Parse(
                    tKeyIn6.Text,
                    System.Globalization.NumberStyles.HexNumber
                );

                SendLen = 0x0B; // Panjang data yang dikirim
                RecvLen = 0x02; // Panjang data yang diterima

                retCode = SendAPDUandDisplay(0);

                // Jika ada kesalahan, keluar dari fungsi
                if (retCode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    return;
                }

                // Gunakan memori volatil untuk otentikasi
                ClearBuffers();
                SendBuff[0] = 0xFF; // CLA
                SendBuff[1] = 0x86; // INS: Otentikasi dengan kunci yang tersimpan
                SendBuff[2] = 0x00; // P1
                SendBuff[3] = 0x00; // P2
                SendBuff[4] = 0x05; // P3: Panjang data otentikasi
                SendBuff[5] = 0x01; // Byte 1: Versi
                SendBuff[6] = 0x00; // Byte 2
                SendBuff[7] = (byte)int.Parse(tBlkNo.Text); // Byte 3: Nomor blok

                // Pilih jenis kunci (Key A atau Key B)
                if (rbKType1.Checked == true)
                {
                    SendBuff[8] = 0x60; // Key A
                }
                else
                {
                    SendBuff[8] = 0x61; // Key B
                }

                SendBuff[9] = 0x20; // Byte 5: Kunci sesi untuk memori volatil
            }
            else
            {
                // Otentikasi dengan sumber lain (non-volatil)
                ClearBuffers();
                SendBuff[0] = 0xFF; // CLA
                SendBuff[1] = 0x86; // INS: Otentikasi dengan kunci yang tersimpan
                SendBuff[2] = 0x00; // P1
                SendBuff[3] = 0x00; // P2
                SendBuff[4] = 0x05; // P3
                SendBuff[5] = 0x01; // Byte 1: Versi
                SendBuff[6] = 0x00; // Byte 2
                SendBuff[7] = (byte)int.Parse(tBlkNo.Text); // Byte 3: Nomor blok

                // Pilih jenis kunci (Key A atau Key B)
                if (rbKType2.Checked == true)
                {
                    SendBuff[8] = 0x61; // Key A
                }
                else
                {
                    SendBuff[8] = 0x60; // Key B
                }

                // Gunakan kunci sesi berdasarkan sumber
                if (rbSource2.Checked == true)
                {
                    SendBuff[9] = 0x20; // Kunci sesi untuk memori volatil
                }
                else
                {
                    SendBuff[9] = (byte)int.Parse(tKeyAdd.Text); // Kunci sesi untuk memori non-volatil
                }
            }

            SendLen = 0x0A; // Panjang data yang dikirim
            RecvLen = 0x02; // Panjang data yang diterima

            retCode = SendAPDUandDisplay(0);

            // Jika ada kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bValStor_Click(object sender, EventArgs e)
        {
            long Amount; // Variabel untuk menyimpan jumlah nilai yang akan disimpan
            int tempInt; // Variabel sementara untuk validasi input angka

            // Validasi: Pastikan jumlah nilai (tValAmt) diisi dan merupakan angka
            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {
                tValAmt.Focus();
                tValAmt.Text = "";
                return;
            }

            // Validasi: Pastikan nilai yang dimasukkan tidak melebihi 4294967295 (batas 4-byte unsigned integer)
            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {
                tValAmt.Text = "4294967295"; // Jika lebih, set ke batas maksimum
                tValAmt.Focus();
                return;
            }

            // Validasi: Pastikan nomor blok (tValBlk) diisi dan merupakan angka
            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {
                tValBlk.Focus();
                tValBlk.Text = "";
                return;
            }

            // Validasi: Pastikan nomor blok tidak melebihi 319
            if (int.Parse(tValBlk.Text) > 319)
            {
                tValBlk.Text = "319";
                return;
            }

            // Mengosongkan field sumber dan target sebelum menyimpan nilai
            tValSrc.Text = "";
            tValTar.Text = "";

            // Konversi jumlah nilai ke tipe long
            Amount = Convert.ToInt64(tValAmt.Text);

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk menyimpan nilai ke kartu
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xD7; // INS (Instruction: Store Value)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tValBlk.Text); // P2 (Nomor blok tempat menyimpan nilai)
            SendBuff[4] = 0x05; // Lc (Jumlah byte yang akan dikirim)
            SendBuff[5] = 0x00; // VB_OP Value (Operasi nilai)

            // Memecah nilai Amount menjadi 4 byte untuk dikirim ke kartu
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF); // Byte ke-1 (paling signifikan)
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF); // Byte ke-2
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF); // Byte ke-3
            SendBuff[9] = (byte)(Amount & 0xFF); // Byte ke-4 (paling tidak signifikan)

            // Menentukan panjang data yang dikirim dan panjang respons yang diharapkan
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02; // Panjang respons (Status Word)

            // Mengirim perintah ke kartu dan menampilkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bValInc_Click(object sender, EventArgs e)
        {
            long Amount; // Variabel untuk menyimpan jumlah nilai yang akan ditambahkan
            int tempInt; // Variabel sementara untuk validasi input angka

            // Validasi: Pastikan jumlah nilai (tValAmt) diisi dan merupakan angka
            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {
                tValAmt.Focus();
                tValAmt.Text = "";
                return;
            }

            // Validasi: Pastikan nilai yang dimasukkan tidak melebihi 4294967295 (batas 4-byte unsigned integer)
            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {
                tValAmt.Text = "4294967295"; // Jika lebih, set ke batas maksimum
                tValAmt.Focus();
                return;
            }

            // Validasi: Pastikan nomor blok (tValBlk) diisi dan merupakan angka
            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {
                tValBlk.Focus();
                tValBlk.Text = "";
                return;
            }

            // Validasi: Pastikan nomor blok tidak melebihi 319
            if (int.Parse(tValBlk.Text) > 319)
            {
                tValBlk.Text = "319";
                return;
            }

            // Mengosongkan field sumber dan target sebelum menambahkan nilai
            tValSrc.Text = "";
            tValTar.Text = "";

            // Konversi jumlah nilai ke tipe long
            Amount = Convert.ToInt64(tValAmt.Text);

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk menambahkan nilai ke kartu
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xD7; // INS (Instruction: Increase Value)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tValBlk.Text); // P2 (Nomor blok tempat menambahkan nilai)
            SendBuff[4] = 0x05; // Lc (Jumlah byte yang akan dikirim)
            SendBuff[5] = 0x01; // VB_OP Value (1 = Increment Value)

            // Memecah nilai Amount menjadi 4 byte untuk dikirim ke kartu
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF); // Byte ke-1 (paling signifikan)
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF); // Byte ke-2
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF); // Byte ke-3
            SendBuff[9] = (byte)(Amount & 0xFF); // Byte ke-4 (paling tidak signifikan)

            // Menentukan panjang data yang dikirim dan panjang respons yang diharapkan
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02; // Panjang respons (Status Word)

            // Mengirim perintah ke kartu dan menampilkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bValDec_Click(object sender, EventArgs e)
        {
            long Amount; // Variabel untuk menyimpan jumlah nilai yang akan dikurangi
            int tempInt; // Variabel sementara untuk validasi input angka

            // Validasi: Pastikan jumlah nilai (tValAmt) diisi dan merupakan angka
            if (tValAmt.Text == "" | !int.TryParse(tValAmt.Text, out tempInt))
            {
                tValAmt.Focus();
                tValAmt.Text = "";
                return;
            }

            // Validasi: Pastikan nilai yang dimasukkan tidak melebihi 4294967295 (batas maksimum 4-byte unsigned integer)
            if (Convert.ToInt64(tValAmt.Text) > 4294967295)
            {
                tValAmt.Text = "4294967295"; // Jika lebih, set ke batas maksimum
                tValAmt.Focus();
                return;
            }

            // Validasi: Pastikan nomor blok (tValBlk) diisi dan merupakan angka
            if (tValBlk.Text == "" | !int.TryParse(tValBlk.Text, out tempInt))
            {
                tValBlk.Focus();
                tValBlk.Text = "";
                return;
            }

            // Validasi: Pastikan nomor blok tidak melebihi 319
            if (int.Parse(tValBlk.Text) > 319)
            {
                tValBlk.Text = "319";
                return;
            }

            // Mengosongkan field sumber dan target sebelum mengurangi nilai
            tValSrc.Text = "";
            tValTar.Text = "";

            // Konversi jumlah nilai ke tipe long
            Amount = Convert.ToInt64(tValAmt.Text);

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk mengurangi nilai dari kartu
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xD7; // INS (Instruction: Decrease Value)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tValBlk.Text); // P2 (Nomor blok tempat mengurangi nilai)
            SendBuff[4] = 0x05; // Lc (Jumlah byte yang akan dikirim)
            SendBuff[5] = 0x02; // VB_OP Value (2 = Decrement Value)

            // Memecah nilai Amount menjadi 4 byte untuk dikirim ke kartu
            SendBuff[6] = (byte)((Amount >> 24) & 0xFF); // Byte ke-1 (paling signifikan)
            SendBuff[7] = (byte)((Amount >> 16) & 0xFF); // Byte ke-2
            SendBuff[8] = (byte)((Amount >> 8) & 0xFF); // Byte ke-3
            SendBuff[9] = (byte)(Amount & 0xFF); // Byte ke-4 (paling tidak signifikan)

            // Menentukan panjang data yang dikirim dan panjang respons yang diharapkan
            SendLen = SendBuff[4] + 5;
            RecvLen = 0x02; // Panjang respons (Status Word)

            // Mengirim perintah ke kartu dan menampilkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bValRead_Click(object sender, EventArgs e)
        {
            long Amount; // Variabel untuk menyimpan nilai yang dibaca dari kartu

            // Validasi: Pastikan nomor blok tidak melebihi 319
            if (int.Parse(tValBlk.Text) > 319)
            {
                tValBlk.Text = "319";
                return;
            }

            // Kosongkan field sebelum membaca nilai dari kartu
            tValAmt.Text = "";
            tValSrc.Text = "";
            tValTar.Text = "";

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk membaca nilai dari blok kartu
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xB1; // INS (Instruction: Read Value)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tValBlk.Text); // P2 : Nomor blok yang akan dibaca
            SendBuff[4] = 0x00; // Le (Panjang data yang diminta)

            SendLen = 0x05; // Panjang perintah yang dikirim (5 byte)
            RecvLen = 0x06; // Panjang respons yang diharapkan (6 byte)

            // Mengirim perintah ke kartu dan mendapatkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            // Mengonversi data yang diterima menjadi nilai dalam format integer 4-byte
            Amount = RecvBuff[3]; // Byte ke-4 (paling tidak signifikan)
            Amount = Amount + (RecvBuff[2] * 256); // Byte ke-3
            Amount = Amount + (RecvBuff[1] * 256 * 256); // Byte ke-2
            Amount = Amount + (RecvBuff[0] * 256 * 256 * 256); // Byte ke-1 (paling signifikan)

            // Menampilkan hasil pembacaan nilai ke dalam field tValAmt
            tValAmt.Text = Amount.ToString();
        }

        private void bValRes_Click(object sender, EventArgs e)
        {
            int tempInt;

            // Validasi input untuk memastikan tidak ada nilai kosong dan format angka benar
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

            // Validasi: Pastikan nomor blok sumber tidak lebih dari 319
            if (int.Parse(tValSrc.Text) > 319)
            {
                tValSrc.Text = "319";
                return;
            }

            // Validasi: Pastikan nomor blok target tidak lebih dari 319
            if (int.Parse(tValTar.Text) > 319)
            {
                tValTar.Text = "319";
                return;
            }

            // Kosongkan nilai blok dan jumlah nilai sebelum proses transfer
            tValAmt.Text = "";
            tValBlk.Text = "";

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk melakukan restore nilai dari blok sumber ke blok target
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xD7; // INS (Instruction: Restore Value)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tValSrc.Text); // P2 : Nomor blok sumber
            SendBuff[4] = 0x02; // Lc : Panjang data
            SendBuff[5] = 0x03; // Data In Byte 1 (Kode operasi Restore)
            SendBuff[6] = (byte)int.Parse(tValTar.Text); // P2 : Nomor blok tujuan

            SendLen = 0x07; // Panjang perintah yang dikirim (7 byte)
            RecvLen = 0x02; // Panjang respons yang diharapkan (2 byte)

            // Mengirim perintah ke kartu dan mendapatkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        private void bHexRead_Click(object sender, EventArgs e)
        {
            string tmpStr;

            // Mengosongkan field tampilan data biner sebelum membaca
            tbHextoStr.Text = "";

            // Validasi: Pastikan nomor blok diisi
            if (tBinBlk.Text == "")
            {
                tBinBlk.Focus();
                return;
            }

            // Validasi: Pastikan nomor blok tidak melebihi 319
            if (int.Parse(tBinBlk.Text) > 319)
            {
                tBinBlk.Text = "319";
                return;
            }

            // Validasi: Pastikan panjang data diisi
            if (tBinLen.Text == "")
            {
                tBinLen.Focus();
                return;
            }

            // Mengosongkan buffer sebelum mengirim perintah baru
            ClearBuffers();

            // Menyiapkan perintah APDU untuk membaca data dari kartu
            SendBuff[0] = 0xFF; // CLA (Class of Instruction)
            SendBuff[1] = 0xB0; // INS (Instruction: Read Binary)
            SendBuff[2] = 0x00; // P1 (Parameter 1)
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text); // P2 (Nomor blok yang akan dibaca)
            SendBuff[4] = (byte)int.Parse(tBinLen.Text); // P3 (Jumlah byte yang akan dibaca)

            // Menentukan panjang data yang dikirim dan diterima
            SendLen = 5;
            RecvLen = SendBuff[4] + 2; // Ditambah 2 untuk byte status respons

            // Mengirim perintah ke kartu dan menampilkan respons
            retCode = SendAPDUandDisplay(2);

            // Jika terjadi kesalahan, keluar dari fungsi
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            // Mengubah data yang diterima menjadi string dan menampilkannya
            tmpStr = "";

            tmpStr = ByteArrayToString(RecvBuff.Take(RecvLen - 2).ToArray());

            // Menampilkan data yang dibaca ke dalam text field
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

        //private void bHexUpd_Click(object sender, EventArgs e)
        //{
        //    int targetBlock;
        //    if (!int.TryParse(tTargetBlk.Text, out targetBlock))
        //    {
        //        tTargetBlk.Focus();
        //        return;
        //    }

        //    if (targetBlock < 0 || targetBlock > 319)
        //    {
        //        return;
        //    }

        //    byte[] accessBits = new byte[4];
        //    if (!byte.TryParse(tAC1.Text, System.Globalization.NumberStyles.HexNumber, null, out accessBits[0]) ||
        //        !byte.TryParse(tAC2.Text, System.Globalization.NumberStyles.HexNumber, null, out accessBits[1]) ||
        //        !byte.TryParse(tAC3.Text, System.Globalization.NumberStyles.HexNumber, null, out accessBits[2]) ||
        //        !byte.TryParse(tAC4.Text, System.Globalization.NumberStyles.HexNumber, null, out accessBits[3]))
        //    {
        //        return;
        //    }

        //    ClearBuffers();

        //    SendBuff = new byte[] { 0xFF, 0x82, 0x00, 0x00, 0x06, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        //                        //  CLA,  INS,  P1,   P2,   LC,     DATA Load Key A (FFFFFFFFFFFF)
        //    SendLen = SendBuff.Length;
        //    RecvLen = 2;

        //    if (SendAPDUandDisplay(0) != ModWinsCard.SCARD_S_SUCCESS)
        //    {
        //        return;
        //    }

        //    SendBuff = new byte[] { 0xFF, 0x86, 0x00, 0x00, 0x05, 0x01, 0x00, (byte)targetBlock, 0x60, 0x00 };
        //                        //  CLA,  INS,  P1,   P2,   LC,   Number of keys, Key slot, Target, Key A/B (60/61), Slot Key
        //    SendLen = SendBuff.Length;
        //    RecvLen = 2;

        //    if (SendAPDUandDisplay(0) != ModWinsCard.SCARD_S_SUCCESS)
        //    {
        //        return;
        //    }

        //    //byte[] authBlockData = {
        //    //    // Key A
        //    //    0x0A, 0x1A, 0x2A, 0x3A, 0x4A, 0x5A,
        //    //    // Access Bits
        //    //    accessBits[0], accessBits[1], accessBits[2], accessBits[3],
        //    //    // Key B
        //    //    0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5
        //    //};

        //    byte[] authBlockData = {
        //        // Key A
        //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
        //        // Access Bits
        //        accessBits[0], accessBits[1], accessBits[2], accessBits[3],
        //        // Key B
        //        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
        //    };

        //    // Membuat APDU untuk Update Access Bits
        //    SendBuff = new byte[21];
        //    SendBuff[0] = 0xFF;
        //    SendBuff[1] = 0xD6;
        //    SendBuff[2] = 0x00;
        //    SendBuff[3] = (byte)targetBlock;
        //    SendBuff[4] = 0x10;

        //    Array.Copy(authBlockData, 0, SendBuff, 5, 16);
        //    SendLen = SendBuff.Length;
        //    RecvLen = 2;

        //    if (SendAPDUandDisplay(0) != ModWinsCard.SCARD_S_SUCCESS)
        //    {
        //        return;
        //    }

        //    MessageBox.Show($"Access Bits berhasil diperbarui di sektor {targetBlock / 4} (blok {targetBlock})!",
        //                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

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
            SendBuff[0] = 0xFF; // CLA
            SendBuff[1] = 0xD6; // INS
            SendBuff[2] = 0x00; // P1
            SendBuff[3] = (byte)int.Parse(tBinBlk.Text); // P2 : Starting Block No.
            SendBuff[4] = (byte)int.Parse(tBinLen.Text); // P3 : Data length

            Array.Copy(byteArray, 0, SendBuff, 5, byteArray.Length);

            SendLen = SendBuff[4] + 5;
            RecvLen = 0x00;

            retCode = SendAPDUandDisplay(2);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        //private void bReadAll_Click(object sender, EventArgs e)
        //{
        //    // Kosongkan DataGridView sebelum membaca data baru
        //    dReadAll.Rows.Clear();

        //    // Loop untuk membaca semua blok yang tersedia (0-319)
        //    for (int block = 0; block <= 319; block++)
        //    {
        //        string tmpStr;

        //        // Mengosongkan buffer sebelum mengirim perintah baru
        //        ClearBuffers();

        //        // Menyiapkan perintah APDU untuk membaca data dari kartu
        //        SendBuff[0] = 0xFF; // CLA (Class of Instruction)
        //        SendBuff[1] = 0xB0; // INS (Instruction: Read Binary)
        //        SendBuff[2] = 0x00; // P1 (Parameter 1)
        //        SendBuff[3] = (byte)block; // P2 (Nomor blok yang akan dibaca)
        //        SendBuff[4] = 16; // P3 (Jumlah byte yang akan dibaca per blok, bisa disesuaikan)

        //        // Menentukan panjang data yang dikirim dan diterima
        //        SendLen = 5;
        //        RecvLen = SendBuff[4] + 2; // Ditambah 2 untuk byte status respons

        //        // Mengirim perintah ke kartu dan menampilkan respons
        //        retCode = SendAPDUandDisplay(2);

        //        // Jika terjadi kesalahan, lanjut ke blok berikutnya
        //        if (retCode != ModWinsCard.SCARD_S_SUCCESS)
        //        {
        //            continue;
        //        }

        //        // Mengubah data yang diterima menjadi string
        //        tmpStr = ByteArrayToString(RecvBuff.Take(RecvLen - 2).ToArray()).ToUpper();

        //        // Menambahkan hasil pembacaan ke dalam DataGridView
        //        dReadAll.Rows.Add(block, tmpStr);
        //    }
        //}

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
            }; //256 blocks (4K card)
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
            string ReaderList = "" + Convert.ToChar(0); // Inisialisasi daftar pembaca kartu
            int indx;
            int pcchReaders = 0;
            string rName = "";

            // 1. Membangun konteks untuk komunikasi dengan smart card
            retCode = ModWinsCard.SCardEstablishContext(
                ModWinsCard.SCARD_SCOPE_USER,
                0,
                0,
                ref hContext
            );

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(1, retCode, ""); // Menampilkan pesan kesalahan jika gagal
                return;
            }

            // 2. Mendapatkan daftar pembaca kartu PC/SC yang tersedia di sistem
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(1, retCode, ""); // Menampilkan pesan kesalahan jika gagal mendapatkan daftar pembaca kartu
                return;
            }

            EnableButtons(); // Mengaktifkan tombol-tombol yang diperlukan

            byte[] ReadersList = new byte[pcchReaders]; // Buffer untuk daftar pembaca kartu

            // Mengisi daftar pembaca kartu
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

            // Mengonversi buffer daftar pembaca kartu menjadi string
            while (ReadersList[indx] != 0)
            {
                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx]; // Menyusun nama pembaca kartu karakter per karakter
                    indx = indx + 1;
                }

                // Menambahkan nama pembaca kartu ke dalam combobox
                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;
            }

            // Jika ada pembaca kartu yang tersedia, pilih yang pertama
            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 0;
            }

            indx = 1;

            // Mencari pembaca kartu ACR128 PICC dan menjadikannya sebagai pembaca default
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
            // Menghubungkan ke pembaca kartu yang dipilih menggunakan handle hContext untuk mendapatkan handle hCard
            if (connActive)
            {
                // Jika sudah terhubung sebelumnya, putuskan koneksi sebelum menyambungkan ulang
                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            }

            // Membuka koneksi dengan mode eksklusif ke smart card
            retCode = ModWinsCard.SCardConnect(
                hContext,
                cbReader.Text,
                ModWinsCard.SCARD_SHARE_EXCLUSIVE,
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

            connActive = true; // Menandai bahwa koneksi ke smart card aktif
            gbLoadKeys.Enabled = true; // Mengubah group box Load Key menjadi aktif saat connect diklik
            gbAuth.Enabled = true; // Mengubah group box Auth menjadi aktif saat connect diklik
            gbBinOps.Enabled = true; // Mengubah group box Binary Ops menjadi aktif saat connect diklik
            gbValBlk.Enabled = true; // Mengubah group box Value Block menjadi aktif saat connect diklik
            rbSource1.Checked = true; // Memberikan nilai default kepada pilihan radio box
            rbKType1.Checked = true; // Memberikan nilai default kepada pilihan radio box
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            // Menghapus semua pesan di daftar pesan UI
            mMsg.Items.Clear();
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            // Memutuskan koneksi jika masih terhubung
            if (connActive)
            {
                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            }

            // Melepaskan konteks komunikasi dengan smart card
            retCode = ModWinsCard.SCardReleaseContext(hCard);

            InitMenu(); // Mengatur ulang UI ke kondisi awal
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            // Mengakhiri aplikasi
            retCode = ModWinsCard.SCardReleaseContext(hContext); // Melepaskan konteks komunikasi dengan kartu
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD); // Memutus koneksi dengan kartu
            System.Environment.Exit(0); // Keluar dari aplikasi
        }

        private int SendAPDUandDisplay(int reqType)
        {
            int indx;
            string tmpStr;

            // Mengatur protokol dan panjang data untuk pengiriman
            pioSendRequest.dwProtocol = Aprotocol;
            pioSendRequest.cbPciLength = 8;

            // Menampilkan perintah APDU yang akan dikirim
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]); // Format data dalam bentuk heksadesimal
            }

            displayOut(2, 0, tmpStr); // Menampilkan APDU yang dikirim
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
                displayOut(1, retCode, ""); // Menampilkan pesan kesalahan jika transmisi gagal
                return retCode;
            }
            else
            {
                tmpStr = "";
                switch (reqType)
                {
                    case 0: // Cek kode status (SW1 SW2)
                        for (indx = (RecvLen - 2); indx <= (RecvLen - 1); indx++)
                        {
                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                        }

                        if ((tmpStr).Trim() != "90 00")
                        {
                            displayOut(4, 0, "Return bytes are not acceptable.");
                        }
                        break;

                    case 1: // Membaca ATR (Answer to Reset)
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

                    case 2: // Membaca seluruh data yang diterima
                        for (indx = 0; indx <= (RecvLen - 1); indx++)
                        {
                            tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);
                        }
                        break;
                }

                displayOut(3, 0, tmpStr.Trim()); // Menampilkan data yang diterima dari kartu
            }

            return retCode;
        }

        private void btnGetUID_Click(object sender, EventArgs e)
        {
            ClearBuffers();

            // Mengatur perintah APDU untuk membaca UID kartu
            SendBuff[0] = 0xFF; // CLA
            SendBuff[1] = 0xCA; // INS
            SendBuff[2] = 0x00; // P1
            SendBuff[3] = 0x00; // P2
            SendBuff[4] = 0x00; // Le (Expected length of response)

            SendLen = 5; // Panjang dikirim
            RecvLen = 10; // Panjang diterima

            retCode = SendAPDUandDisplay(2); // Mengirim APDU dan menampilkan hasil

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }
        }

        //private void CalculateAccessBits()
        //{
        //    if (dgvSectorTrailer.SelectedRows.Count == 0)
        //    {
        //        MessageBox.Show("Pilih satu baris dari tabel Sector Trailer.");
        //        return;
        //    }

        //    int selectedIndex = dgvSectorTrailer.SelectedRows[0].Index;
        //    SectorTrailerCondition selectedCondition = accessConditionsST[selectedIndex];

        //    // Contoh konversi ke Access Bits
        //    string ab1 = ConvertToBit(selectedCondition.KeyARead);
        //    string ab2 = ConvertToBit(selectedCondition.KeyAWrite);
        //    string ab3 = ConvertToBit(selectedCondition.AccessBitRead);
        //    string ab4 = ConvertToBit(selectedCondition.AccessBitWrite);

        //    // Menampilkan hasil di TextBox
        //    tAB1.Text = ab1;
        //    tAB2.Text = ab2;
        //    tAB3.Text = ab3;
        //    tAB4.Text = ab4;
        //}

        //private string ConvertToBit(string access)
        //{
        //    switch (access)
        //    {
        //        case "Never":
        //            return "00";
        //        case "Key A":
        //            return "01";
        //        case "Key B":
        //            return "10";
        //        case "Key A | B":
        //            return "11";
        //        default:
        //            return "00";
        //    }
        //}

        //private void InitializeTabControl()
        //{
        //    accessConditionsBlock0 = new BindingList<DataBlockCondition>
        //    {
        //        new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
        //        new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY B", "Never", "Never", "Never"),
        //        new DataBlockCondition("Never", "Never", "Never", "Never"),
        //    };

        //    accessConditionsBlock1 = new BindingList<DataBlockCondition>
        //    {
        //        new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
        //        new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY B", "Never", "Never", "Never"),
        //        new DataBlockCondition("Never", "Never", "Never", "Never"),
        //    };

        //    accessConditionsBlock2 = new BindingList<DataBlockCondition>
        //    {
        //        new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
        //        new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
        //        new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
        //        new DataBlockCondition("KEY B", "Never", "Never", "Never"),
        //        new DataBlockCondition("Never", "Never", "Never", "Never"),
        //    };

        //    accessConditionsST = new BindingList<SectorTrailerCondition>
        //    {
        //        new SectorTrailerCondition("Never", "KEY A", "KEY A", "Never", "KEY A", "KEY A"),
        //        new SectorTrailerCondition("Never", "Never", "KEY A", "Never", "KEY A", "Never"),
        //        new SectorTrailerCondition(
        //            "Never",
        //            "KEY B",
        //            "KEY A | B",
        //            "Never",
        //            "Never",
        //            "KEY B"
        //        ),
        //        new SectorTrailerCondition(
        //            "Never",
        //            "Never",
        //            "KEY A | B",
        //            "Never",
        //            "Never",
        //            "Never"
        //        ),
        //        new SectorTrailerCondition("Never", "KEY A", "KEY A", "KEY A", "KEY A", "KEY A"),
        //        new SectorTrailerCondition(
        //            "Never",
        //            "KEY B",
        //            "KEY A | B",
        //            "KEY B",
        //            "Never",
        //            "KEY B"
        //        ),
        //        new SectorTrailerCondition(
        //            "Never",
        //            "Never",
        //            "KEY A | B",
        //            "KEY B",
        //            "Never",
        //            "Never"
        //        ),
        //        new SectorTrailerCondition(
        //            "Never",
        //            "Never",
        //            "KEY A | B",
        //            "Never",
        //            "Never",
        //            "Never"
        //        ),
        //    };

        //    AddDataGridViewToTab(tpBlock0, accessConditionsBlock0);
        //    AddDataGridViewToTab(tpBlock1, accessConditionsBlock1);
        //    AddDataGridViewToTab(tpBlock2, accessConditionsBlock2);
        //    AddDataGridViewSectorTrailer(tpST, accessConditionsST);
        //}

        //private void AddDataGridViewToTab(TabPage tab, BindingList<DataBlockCondition> conditions)
        //{
        //    DataGridView dgvDataBlock = new DataGridView
        //    {
        //        Dock = DockStyle.Fill,
        //        DataSource = new BindingList<DataBlockCondition>(conditions),
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
        //        AllowUserToAddRows = false,
        //        ReadOnly = true,
        //    };

        //    if (dgvDataBlock.Columns["IsSelected"] != null)
        //        dgvDataBlock.Columns["IsSelected"].Visible = true;

        //    DataGridViewCheckBoxColumn radioColumn = new DataGridViewCheckBoxColumn
        //    {
        //        HeaderText = "",
        //        DataPropertyName = "IsSelected",
        //        Width = 30,
        //        ReadOnly = false,
        //    };
        //    dgvDataBlock.Columns.Insert(0, radioColumn);

        //    dgvDataBlock.CellClick += (sender, e) =>
        //    {
        //        if (e.ColumnIndex == 0)
        //        {
        //            foreach (var item in conditions)
        //            {
        //                item.IsSelected = false;
        //            }
        //            conditions[e.RowIndex].IsSelected = true;
        //            dgvDataBlock.Refresh();
        //        }
        //    };

        //    tab.Controls.Clear();
        //    tab.Controls.Add(dgvDataBlock);
        //}

        //private void AddDataGridViewSectorTrailer(
        //    TabPage tab,
        //    BindingList<SectorTrailerCondition> conditions
        //)
        //{
        //    DataGridView dgvSectorTrailer = new DataGridView
        //    {
        //        Dock = DockStyle.Fill,
        //        DataSource = conditions,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
        //        AllowUserToAddRows = false,
        //        ReadOnly = false,
        //    };

        //    DataGridViewCheckBoxColumn radioColumn = new DataGridViewCheckBoxColumn
        //    {
        //        HeaderText = "",
        //        DataPropertyName = "IsSelected",
        //        Width = 30,
        //        ReadOnly = false,
        //    };
        //    dgvSectorTrailer.Columns.Insert(0, radioColumn);

        //    dgvSectorTrailer.CellClick += (sender, e) =>
        //    {
        //        if (e.ColumnIndex == 0)
        //        {
        //            foreach (var item in conditions)
        //            {
        //                item.IsSelected = false;
        //            }
        //            conditions[e.RowIndex].IsSelected = true;
        //            dgvSectorTrailer.Refresh();
        //        }
        //    };

        //    tab.Controls.Clear();
        //    tab.Controls.Add(dgvSectorTrailer);
        //}

        private void InitializeTabControl()
        {
            accessConditionsBlock0 = new BindingList<DataBlockCondition>
            {
                new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
                new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY B", "Never", "Never", "Never"),
                new DataBlockCondition("Never", "Never", "Never", "Never"),
            };

            accessConditionsBlock1 = new BindingList<DataBlockCondition>
            {
                new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
                new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY B", "Never", "Never", "Never"),
                new DataBlockCondition("Never", "Never", "Never", "Never"),
            };

            accessConditionsBlock2 = new BindingList<DataBlockCondition>
            {
                new DataBlockCondition("KEY A | B", "KEY A | B", "KEY A | B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY A | B", "KEY B", "KEY B", "KEY A | B"),
                new DataBlockCondition("KEY A | B", "Never", "Never", "KEY A | B"),
                new DataBlockCondition("KEY B", "KEY B", "Never", "Never"),
                new DataBlockCondition("KEY B", "Never", "Never", "Never"),
                new DataBlockCondition("Never", "Never", "Never", "Never"),
            };

            accessConditionsST = new BindingList<SectorTrailerCondition>
            {
                new SectorTrailerCondition("Never", "KEY A", "KEY A", "Never", "KEY A", "KEY A"),
                new SectorTrailerCondition("Never", "Never", "KEY A", "Never", "KEY A", "Never"),
                new SectorTrailerCondition(
                    "Never",
                    "KEY B",
                    "KEY A | B",
                    "Never",
                    "Never",
                    "KEY B"
                ),
                new SectorTrailerCondition(
                    "Never",
                    "Never",
                    "KEY A | B",
                    "Never",
                    "Never",
                    "Never"
                ),
                new SectorTrailerCondition("Never", "KEY A", "KEY A", "KEY A", "KEY A", "KEY A"),
                new SectorTrailerCondition(
                    "Never",
                    "KEY B",
                    "KEY A | B",
                    "KEY B",
                    "Never",
                    "KEY B"
                ),
                new SectorTrailerCondition(
                    "Never",
                    "Never",
                    "KEY A | B",
                    "KEY B",
                    "Never",
                    "Never"
                ),
                new SectorTrailerCondition(
                    "Never",
                    "Never",
                    "KEY A | B",
                    "Never",
                    "Never",
                    "Never"
                ),
            };

            dgvDataBlock0 = new DataGridView();
            dgvDataBlock1 = new DataGridView();
            dgvDataBlock2 = new DataGridView();
            dgvSectorTrailer = new DataGridView();

            AddDataGridViewToTab(tpBlock0, accessConditionsBlock0, dgvDataBlock0);
            AddDataGridViewToTab(tpBlock1, accessConditionsBlock1, dgvDataBlock1);
            AddDataGridViewToTab(tpBlock2, accessConditionsBlock2, dgvDataBlock2);
            AddDataGridViewSectorTrailer(tpST, accessConditionsST, dgvSectorTrailer);
        }

        private void AddDataGridViewToTab(
            TabPage tab,
            BindingList<DataBlockCondition> conditions,
            DataGridView dgvDataBlock
        )
        {
            dgvDataBlock.Dock = DockStyle.Fill;
            dgvDataBlock.DataSource = new BindingList<DataBlockCondition>(conditions);
            dgvDataBlock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDataBlock.AllowUserToAddRows = false;
            dgvDataBlock.ReadOnly = true;

            if (dgvDataBlock.Columns["IsSelected"] != null)
                dgvDataBlock.Columns["IsSelected"].Visible = true;

            DataGridViewCheckBoxColumn radioColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                DataPropertyName = "IsSelected",
                Width = 30,
                ReadOnly = false,
            };
            dgvDataBlock.Columns.Insert(0, radioColumn);

            dgvDataBlock.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    foreach (var item in conditions)
                    {
                        item.IsSelected = false;
                    }
                    conditions[e.RowIndex].IsSelected = true;
                    dgvDataBlock.Refresh();
                }
            };

            tab.Controls.Clear();
            tab.Controls.Add(dgvDataBlock);
        }

        private void AddDataGridViewSectorTrailer(
            TabPage tab,
            BindingList<SectorTrailerCondition> conditions,
            DataGridView dgvSectorTrailer
        )
        {
            dgvSectorTrailer.Dock = DockStyle.Fill;
            dgvSectorTrailer.DataSource = new BindingList<SectorTrailerCondition>(conditions);
            dgvSectorTrailer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSectorTrailer.AllowUserToAddRows = false;
            dgvSectorTrailer.ReadOnly = true;

            if (dgvSectorTrailer.Columns["IsSelected"] != null)
                dgvSectorTrailer.Columns["IsSelected"].Visible = true;

            DataGridViewCheckBoxColumn radioColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                DataPropertyName = "IsSelected",
                Width = 30,
                ReadOnly = false,
            };
            dgvSectorTrailer.Columns.Insert(0, radioColumn);

            dgvSectorTrailer.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    foreach (var item in conditions)
                    {
                        item.IsSelected = false;
                    }
                    conditions[e.RowIndex].IsSelected = true;
                    dgvSectorTrailer.Refresh();
                }
            };

            tab.Controls.Clear();
            tab.Controls.Add(dgvSectorTrailer);
        }

        //private void AddDataGridViewSectorTrailer(
        //    TabPage tab,
        //    BindingList<SectorTrailerCondition> conditions,
        //    DataGridView dgvSectorTrailer
        //)
        //{
        //    dgvSectorTrailer.Dock = DockStyle.Fill;
        //    dgvSectorTrailer.DataSource = conditions;
        //    dgvSectorTrailer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //    dgvSectorTrailer.AllowUserToAddRows = false;
        //    dgvSectorTrailer.ReadOnly = false;

        //    DataGridViewCheckBoxColumn radioColumn = new DataGridViewCheckBoxColumn
        //    {
        //        HeaderText = "",
        //        DataPropertyName = "IsSelected",
        //        Width = 30,
        //        ReadOnly = false,
        //    };
        //    dgvSectorTrailer.Columns.Insert(0, radioColumn);

        //    dgvSectorTrailer.CellClick += (sender, e) =>
        //    {
        //        if (e.ColumnIndex == 0)
        //        {
        //            foreach (var item in conditions)
        //            {
        //                item.IsSelected = false;
        //            }
        //            conditions[e.RowIndex].IsSelected = true;
        //            dgvSectorTrailer.Refresh();
        //        }
        //    };

        //    tab.Controls.Clear();
        //    tab.Controls.Add(dgvSectorTrailer);
        //}

        //private void UpdateAccessBits()
        //{
        //    List<string[]> checkedDataBlock0 = GetCheckedRows(dgvDataBlock0);
        //    List<string[]> checkedDataBlock1 = GetCheckedRows(dgvDataBlock1);
        //    List<string[]> checkedDataBlock2 = GetCheckedRows(dgvDataBlock2);
        //    List<string[]> checkedSectorTrailer = GetCheckedRows(dgvSectorTrailer);

        //    if (bitfieldTable == null || bitfieldTable.Length == 0)
        //    {
        //        MessageBox.Show(
        //            "Error: bitfieldTable belum diinisialisasi!",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error
        //        );
        //        return;
        //    }

        //    int[] access = new int[4];

        //    access[0] =
        //        checkedDataBlock0.Count > 0
        //            ? ConvertToAccessBitsDataBlock(checkedDataBlock0[0])
        //            : 0;
        //    access[1] =
        //        checkedDataBlock1.Count > 0
        //            ? ConvertToAccessBitsDataBlock(checkedDataBlock1[0])
        //            : 0;
        //    access[2] =
        //        checkedDataBlock2.Count > 0
        //            ? ConvertToAccessBitsDataBlock(checkedDataBlock2[0])
        //            : 0;
        //    access[3] =
        //        checkedSectorTrailer.Count > 0
        //            ? ConvertToAccessBitsSectorTrailer(checkedSectorTrailer[0])
        //            : 0;

        //    Debug.WriteLine(
        //        $"Calculated Access: {string.Join(", ", access.Select(x => x.ToString("X2")))}"
        //    );

        //    int c1 =
        //        ((access[3] & 0b100) >> 2) << 7
        //        | ((access[2] & 0b100) >> 2) << 6
        //        | ((access[1] & 0b100) >> 2) << 5
        //        | ((access[0] & 0b100) >> 2) << 4;
        //    int c2 =
        //        ((access[3] & 0b010) >> 1) << 7
        //        | ((access[2] & 0b010) >> 1) << 6
        //        | ((access[1] & 0b010) >> 1) << 5
        //        | ((access[0] & 0b010) >> 1) << 4;
        //    int c3 =
        //        (access[3] & 0b001) << 7
        //        | (access[2] & 0b001) << 6
        //        | (access[1] & 0b001) << 5
        //        | (access[0] & 0b001) << 4;

        //    int byte0 = ~c2 & 0xF0 | (~c1 & 0xF0) >> 4;
        //    int byte1 = c1 | (~c3 & 0xF0) >> 4;
        //    int byte2 = c3 | c2 >> 4;

        //    if (accessBitTextBoxes.Length >= 4)
        //    {
        //        accessBitTextBoxes[0].Text = byte0.ToString("X2");
        //        accessBitTextBoxes[1].Text = byte1.ToString("X2");
        //        accessBitTextBoxes[2].Text = byte2.ToString("X2");
        //        accessBitTextBoxes[3].Text = "00";

        //        Debug.WriteLine(
        //            $"Access Bits: {accessBitTextBoxes[0].Text} {accessBitTextBoxes[1].Text} {accessBitTextBoxes[2].Text} {accessBitTextBoxes[3].Text}"
        //        );
        //    }
        //    else
        //    {
        //        MessageBox.Show(
        //            "Error: accessBitTextBoxes belum diatur dengan benar!",
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error
        //        );
        //    }
        //}

        private void UpdateAccessBits()
        {
            List<string[]> checkedDataBlock0 = GetCheckedRows(dgvDataBlock0);
            List<string[]> checkedDataBlock1 = GetCheckedRows(dgvDataBlock1);
            List<string[]> checkedDataBlock2 = GetCheckedRows(dgvDataBlock2);
            List<string[]> checkedSectorTrailer = GetCheckedRows(dgvSectorTrailer);

            if (bitfieldTable == null || bitfieldTable.Length == 0)
            {
                MessageBox.Show(
                    "Error: bitfieldTable belum diinisialisasi!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            int[] access = new int[4];

            access[0] =
                checkedDataBlock0.Count > 0
                    ? bitfieldTable[ConvertToAccessBitsDataBlock(checkedDataBlock0[0])]
                    : 0;

            access[1] =
                checkedDataBlock1.Count > 0
                    ? bitfieldTable[ConvertToAccessBitsDataBlock(checkedDataBlock1[0])]
                    : 0;

            access[2] =
                checkedDataBlock2.Count > 0
                    ? bitfieldTable[ConvertToAccessBitsDataBlock(checkedDataBlock2[0])]
                    : 0;

            access[3] =
                checkedSectorTrailer.Count > 0
                    ? ConvertToAccessBitsSectorTrailer(checkedSectorTrailer[0])
                    : 0;

            Debug.WriteLine(
                $"Calculated Access: {string.Join(", ", access.Select(x => x.ToString("X2")))}"
            );

            int byte0,
                byte1,
                byte2;

            byte0 =
                (
                    ((~access[3] & 0x02) << 6)
                    | ((~access[2] & 0x02) << 5)
                    | ((~access[1] & 0x02) << 4)
                    | ((~access[0] & 0x02) << 3)
                    | ((~access[3] & 0x01) << 3) // ni salah
                    | ((~access[2] & 0x01) << 2)
                    | ((~access[1] & 0x01) << 1)
                    | ((~access[0] & 0x01) << 0)
                ) & 0xFF;

            byte1 =
                (
                    ((access[3] & 0x01) << 8)
                    | ((access[2] & 0x01) << 6)
                    | ((access[1] & 0x01) << 5)
                    | ((access[0] & 0x01) << 4)
                    | ((~access[3] & 0x04) << 1)
                    | ((~access[2] & 0x04) << 0)
                    | ((~access[1] & 0x04) >> 1)
                    | ((~access[0] & 0x04) >> 2)
                ) & 0xFF;

            byte2 =
                (
                    ((access[3] & 0x04) << 5)
                    | ((access[2] & 0x04) << 4)
                    | ((access[1] & 0x04) << 3)
                    | ((access[0] & 0x04) << 2)
                    | ((access[3] & 0x02) << 2)
                    | ((access[2] & 0x02) << 1)
                    | ((access[1] & 0x02) << 0)
                    | ((access[0] & 0x02) >> 1)
                ) & 0xFF;

            if (accessBitTextBoxes.Length >= 4)
            {
                accessBitTextBoxes[0].Text = byte0.ToString("X2");
                accessBitTextBoxes[1].Text = byte1.ToString("X2");
                accessBitTextBoxes[2].Text = byte2.ToString("X2");
                accessBitTextBoxes[3].Text = "00";

                Debug.WriteLine(
                    $"Access Bits: {accessBitTextBoxes[0].Text} {accessBitTextBoxes[1].Text} {accessBitTextBoxes[2].Text} {accessBitTextBoxes[3].Text}"
                );
            }
            else
            {
                MessageBox.Show(
                    "Error: accessBitTextBoxes belum diatur dengan benar!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            Debug.WriteLine(
                $"Sector Trailer Raw Data: {string.Join(", ", checkedSectorTrailer.Select(x => string.Join(" ", x)))}"
            );
            Debug.WriteLine(
                $"Sector Trailer Index: {ConvertToAccessBitsSectorTrailer(checkedSectorTrailer[0])}"
            );
            Debug.WriteLine($"Sector Trailer Access Value: {access[3]:X2}");
        }

        private int ConvertToAccessBitsDataBlock(string[] rowData)
        {
            if (rowData == null || rowData.Length < 4)
            {
                return 000;
            }

            string c1 = rowData[0].Trim().ToUpper();
            string c2 = rowData[1].Trim().ToUpper();
            string c3 = rowData[2].Trim().ToUpper();
            string c4 = rowData[3].Trim().ToUpper();

            if (c1 == "NEVER" && c2 == "NEVER" && c3 == "NEVER" && c4 == "NEVER")
                return 111;
            if (c1 == "KEY B" && c2 == "NEVER" && c3 == "NEVER" && c4 == "NEVER")
                return 110;
            if (c1 == "KEY B" && c2 == "KEY B" && c3 == "NEVER" && c4 == "NEVER")
                return 101;
            if (c1 == "KEY A | B" && c2 == "NEVER" && c3 == "NEVER" && c4 == "KEY A | B")
                return 100;
            if (c1 == "KEY A | B" && c2 == "KEY B" && c3 == "KEY B" && c4 == "KEY A | B")
                return 011;
            if (c1 == "KEY A | B" && c2 == "KEY B" && c3 == "NEVER" && c4 == "NEVER")
                return 010;
            if (c1 == "KEY A | B" && c2 == "NEVER" && c3 == "NEVER" && c4 == "NEVER")
                return 001;

            return 000;
        }

        private int ConvertToAccessBitsSectorTrailer(string[] rowData)
        {
            if (rowData == null || rowData.Length < 6)
            {
                return 001;
            }

            string readA = rowData[0].Trim().ToUpper();
            string writeA = rowData[1].Trim().ToUpper();
            string readAccessBits = rowData[2].Trim().ToUpper();
            string writeAccessBits = rowData[3].Trim().ToUpper();
            string readB = rowData[4].Trim().ToUpper();
            string writeB = rowData[5].Trim().ToUpper();

            if (
                readA == "NEVER"
                && writeA == "KEY A"
                && readAccessBits == "KEY A"
                && writeAccessBits == "NEVER"
                && readB == "KEY A"
                && writeB == "KEY A"
            )
                return 000;
            if (
                readA == "NEVER"
                && writeA == "NEVER"
                && readAccessBits == "KEY A"
                && writeAccessBits == "NEVER"
                && readB == "KEY A"
                && writeB == "NEVER"
            )
                return 010;
            if (
                readA == "NEVER"
                && writeA == "KEY B"
                && readAccessBits == "KEY A | B"
                && writeAccessBits == "NEVER"
                && readB == "NEVER"
                && writeB == "KEY B"
            )
                return 100;
            if (
                readA == "NEVER"
                && writeA == "NEVER"
                && readAccessBits == "KEY A | B"
                && writeAccessBits == "NEVER"
                && readB == "NEVER"
                && writeB == "NEVER"
            )
                return 110;
            if (
                readA == "NEVER"
                && writeA == "KEY A"
                && readAccessBits == "KEY A"
                && writeAccessBits == "KEY A"
                && readB == "KEY A"
                && writeB == "KEY A"
            )
                return 001;
            if (
                readA == "NEVER"
                && writeA == "KEY B"
                && readAccessBits == "KEY A | B"
                && writeAccessBits == "KEY B"
                && readB == "NEVER"
                && writeB == "KEY B"
            )
                return 011;
            if (
                readA == "NEVER"
                && writeA == "NEVER"
                && readAccessBits == "KEY A | B"
                && writeAccessBits == "KEY B"
                && readB == "NEVER"
                && writeB == "NEVER"
            )
                return 101;
            if (
                readA == "NEVER"
                && writeA == "NEVER"
                && readAccessBits == "KEY A | B"
                && writeAccessBits == "NEVER"
                && readB == "NEVER"
                && writeB == "NEVER"
            )
                return 111;

            return 001;
        }

        private List<string[]> GetCheckedRows(DataGridView dgv)
        {
            List<string[]> checkedRows = new List<string[]>();

            if (dgv == null)
            {
                Trace.WriteLine("DataGridView is null!");
                return checkedRows;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells.Count == 0 || row.Cells[0].Value == null)
                    continue;

                DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                {
                    string[] rowData = new string[row.Cells.Count - 1];

                    for (int i = 1; i < row.Cells.Count; i++)
                    {
                        rowData[i - 1] = row.Cells[i].Value?.ToString() ?? "";
                    }

                    checkedRows.Add(rowData);
                }
            }

            Trace.WriteLine($"Checked Rows Count: {checkedRows.Count}");
            return checkedRows;
        }

        private void btnCalculateAccessBits_Click(object sender, EventArgs e)
        {
            UpdateAccessBits();
        }

        public class DataBlockCondition
        {
            public bool IsSelected { get; set; }
            public string Read { get; set; }
            public string Write { get; set; }
            public string Increment { get; set; }
            public string DecTransferRestore { get; set; }

            public DataBlockCondition(
                string read,
                string write,
                string increment,
                string decTransferRestore
            )
            {
                IsSelected = false;
                Read = read;
                Write = write;
                Increment = increment;
                DecTransferRestore = decTransferRestore;
            }
        }

        public class SectorTrailerCondition
        {
            public bool IsSelected { get; set; }
            public string KeyARead { get; set; }
            public string KeyAWrite { get; set; }
            public string AccessBitRead { get; set; }
            public string AccessBitWrite { get; set; }
            public string KeyBRead { get; set; }
            public string KeyBWrite { get; set; }

            public SectorTrailerCondition(
                string keyARead,
                string keyAWrite,
                string accessBitRead,
                string accessBitWrite,
                string keyBRead,
                string keyBWrite
            )
            {
                IsSelected = false;
                KeyARead = keyARead;
                KeyAWrite = keyAWrite;
                AccessBitRead = accessBitRead;
                AccessBitWrite = accessBitWrite;
                KeyBRead = keyBRead;
                KeyBWrite = keyBWrite;
            }
        }
    }
}
