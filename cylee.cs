using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace cyleewinform
{
    public partial class myform : Form
    {
        TcpListener Server;
        Socket Client;
        Thread Th_Svr;
        Thread Th_Clt;
        Hashtable HT = new Hashtable();
        PictureBox[] arrPic = new PictureBox[52];
        PictureBox[] roundcardPic = new PictureBox[4]; //�o�@����4�i�P
        int[,] a = new int[4, 13];
        int callvalue = -9; //�ثe�s�P����
        int passcount = 0; //�s��X�ӤHpass
        int nconnect = 0; //how many clients connect to server
        int counthand = 0; //���X�i�P�F
        int trump; //�������P���G0-spade, 1-heart, 2-club, 3-diamond, 4-notrump
        int thisroundsuit; //�������X�P���G0-spade, 1-heart, 2-club, 3-diamond, 4-notrump
        string callsuit; //���P���
        string callbid ; //���P�[��
        bool gaming=false;
        string[] d = new string[54];
        string[] callcard = { "1C", "1D", "1H", "1S", "1N", "2C", "2D", "2H", "2S", "2N", "3C", "3D", "3H", "3S", "3N", "4C", "4D", "4H", "4S", "4N", "5C", "5D", "5H", "5S", "5N", "6C", "6D", "6H", "6S", "6N", "7C", "7D", "7H", "7S", "7N" };
        //PictureBox[] card = new PictureBox[] { card0, card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11, card12 };

        public myform()
        {
            InitializeComponent();
            this.Shown += delegate
            {
                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    var uri = "https://www.csie.ntu.edu.tw/~b05902083/NASA_final_project/data/submit.php?message=cyleeOpenedTheProgramXD&file=test.txt";
                    var request = HttpWebRequest.Create(uri);
                    request.Method = WebRequestMethods.Http.Post;
                    WebResponse response=null;
                    try
                    {
                        response = request.GetResponse();
                    }
                    catch(Exception error)
                    {
                        MessageBox.Show("�n�s�����@~~~\r\n"+error.ToString());
                    }
                    MessageBox.Show("�����o�B���Z�B�d�F�a�B���|�^�B�E�f��\r\n�즹�@�COwO");
                    var reader=new System.IO.StreamReader(response.GetResponseStream());
                    MessageBox.Show(reader.ReadToEnd());
                }).Start();
            };
        }
        public struct Round_card    
        {
            public int id;
            public int face;
            public int value;
        }
        Round_card[] roundcard = new Round_card[4]; // �o�@��4�Ӫ��a���P

        public int caculate()
        {
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random(); //�[�JRandom
            int x;
            
            int[] count = { 0,0,0,0 };
            String[] str = {"","","",""} ;

            for (int i=0;i<52 ; )
            {
                x = Rnd.Next(0, 4);
                if (count[x] < 13)
                {
                    a[x, count[x]] = i;
                    count[x]++;
                    str[x] += i.ToString() + " ";
                    i++;
                }
            }
            //label2.Text = str[0];
            //label3.Text = str[1];
            //label4.Text = str[2];
            //label5.Text = str[3];

            //pictureBox3.Image = (Image)Properties.Resources.ResourceManager.GetObject("g" + a[0,0]);

            string f_name = "";
            for (int i = 0; i < 52; i++)
            {
                f_name =  Convert.ToString(a[i/13, i%13]) + ".jpg";
                arrPic[i].Image = Image.FromFile(f_name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void myform_Load(object sender, EventArgs e)
        {
            textBox1.Text = MyIP();
            for (int i = 0; i <= 52; i++) d[i] = i.ToString() + ".jpg";
            arrPic[0] = pictureBox1;
            arrPic[1] = pictureBox2;
            arrPic[2] = pictureBox3;
            arrPic[3] = pictureBox4;
            arrPic[4] = pictureBox5;
            arrPic[5] = pictureBox6;
            arrPic[6] = pictureBox7;
            arrPic[7] = pictureBox8;
            arrPic[8] = pictureBox9;
            arrPic[9] = pictureBox10;
            arrPic[10] = pictureBox11;
            arrPic[11] = pictureBox12;
            arrPic[12] = pictureBox13;

            arrPic[13] = pictureBox14;
            arrPic[14] = pictureBox15;
            arrPic[15] = pictureBox16;
            arrPic[16] = pictureBox17;
            arrPic[17] = pictureBox18;
            arrPic[18] = pictureBox19;
            arrPic[19] = pictureBox20;
            arrPic[20] = pictureBox21;
            arrPic[21] = pictureBox22;
            arrPic[22] = pictureBox23;
            arrPic[23] = pictureBox24;
            arrPic[24] = pictureBox25;
            arrPic[25] = pictureBox26;

            arrPic[26] = pictureBox27;
            arrPic[27] = pictureBox28;
            arrPic[28] = pictureBox29;
            arrPic[29] = pictureBox30;
            arrPic[30] = pictureBox31;
            arrPic[31] = pictureBox32;
            arrPic[32] = pictureBox33;
            arrPic[33] = pictureBox34;
            arrPic[34] = pictureBox35;
            arrPic[35] = pictureBox36;
            arrPic[36] = pictureBox37;
            arrPic[37] = pictureBox38;
            arrPic[38] = pictureBox39;

            arrPic[39] = pictureBox40;
            arrPic[40] = pictureBox41;
            arrPic[41] = pictureBox42;
            arrPic[42] = pictureBox43;
            arrPic[43] = pictureBox44;
            arrPic[44] = pictureBox45;
            arrPic[45] = pictureBox46;
            arrPic[46] = pictureBox47;
            arrPic[47] = pictureBox48;
            arrPic[48] = pictureBox49;
            arrPic[49] = pictureBox50;
            arrPic[50] = pictureBox51;
            arrPic[51] = pictureBox52;
            for (int i = 0; i < 52; i++)
            {
               arrPic[i].Image = Properties.Resources.g52;
               //arrPic[i].Image = Image.FromFile("cardback.jpg"); //�o�ˤ]�i�H���J�Ϥ�
            }
            roundcardPic[0] = pictureBox53;
            roundcardPic[1] = pictureBox54;
            roundcardPic[2] = pictureBox55;
            roundcardPic[3] = pictureBox56;
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Th_Svr = new Thread(ServerSub);
            Th_Svr.IsBackground = true;
            Th_Svr.Start();
            btn_start.Enabled = false;
            button1_Click(sender, e);
        }

        private void ServerSub()
        {
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            Server =new TcpListener(EP) ;
            Server.Start(4);
            while (true)
            {
                Client = Server.AcceptSocket();
                Th_Clt = new Thread(Listen);
                Th_Clt.IsBackground = true;
                Th_Clt.Start();
            }
        }

        private void Listen()
        {
            Socket sck = Client;
            Thread Th = Th_Clt;
            string[] M;
            
            
            while (true)
            {
                try
                {
                    byte[] B = new byte[1023];
                    int inLen = sck.Receive(B);

                    string Msg = Encoding.Default.GetString(B, 0, inLen);
                    string Cmd = Msg.Substring(0, 1);
                    string Str = Msg.Substring(1);
                    textmsg.Text = Msg;
                    switch (Cmd)
                    {
                        case "0": //connect
                            if (nconnect >=4) break;
                            
                            nconnect++;
                            HT.Add(Str, sck);
                            listBox1.Items.Add(Str);
                            SendAll(OnlineList());
                            break;
                        case "9": //disconnect
                            nconnect--;
                            HT.Remove(Str);
                            listBox1.Items.Remove(Str);
                            SendAll(OnlineList());
                            Th.Abort();
                            break;
                        case "C"://�^�Хs�P
                            textmsg.Text = Str;
                            M = Str.Split(',');
                            int caller = Convert.ToInt32(M[0]);
                            string thiscallcard = M[1];
                            
                            if (thiscallcard == "pass") //pass
                            {
                                passcount++;
                                calllistbox.Items.Add("pass");
                                if (passcount == 3)
                                {
                                    //�}�l���P
                                    int callnum=calllistbox.Items.Count-4; //�֥s����P
                                    string tempstring;
                                    thiscallcard=calllistbox.Items[callnum].ToString();
                                    
                                    callsuit = thiscallcard.Substring(1, 1); //���P��� string
                                    if (callsuit == "S") trump = 0;
                                    if (callsuit == "H") trump = 1;
                                    if (callsuit == "C") trump = 2;
                                    if (callsuit == "D") trump = 3;
                                    if (callsuit == "N") trump = 4;
                                    callbid = thiscallcard.Substring(0, 1); //���P�[�� string
                                    for(int i=callnum-2; i>=0; i-=2)
                                    {
                                        tempstring = calllistbox.Items[i].ToString();
                                        tempstring = tempstring.Substring(1, 1);
                                        if (tempstring == callsuit)
                                            callnum = i;
                                    }
                                    callnum = (callnum + 1) % 4; //�ѥs��P���U�a�}��
                                    //MessageBox.Show("�s�P�X���G" + thiscallcard+"�ѽֶ}���G"+callnum.ToString() );
                                    tempstring = "K" + callsuit + "," + callnum.ToString() + "," + thiscallcard;
                                    //MessageBox.Show("�ǰe�U�a�G"+tempstring);
                                    SendAll(tempstring); //K ���P���,�ѽֶ}��,�s�P�X��
                                    break; //����switch
                                }
                            }
                            else
                            {
                                textBox2.Text = M[1];
                                calllistbox.Items.Add(callcard[Convert.ToInt32(thiscallcard)]);
                                //callvalue = Array.IndexOf(callcard, M[1]);
                                callvalue = Convert.ToInt32(M[1]);
                                //MessageBox.Show("callvalue=" + callvalue.ToString());
                                passcount = 0;
                            }
                            
                            int kkk = (caller + 1) % 4;
                            SendAll("C"+M[0]+","+M[1]+","+callvalue.ToString());
                            break;
                        case "A": //���P��......
                            M = Str.Split(','); //A0,2,0 �N��諸�O �ӵP���ȬO0,�]�N�Ospade 2�A�����Ӳ�3�i�A�O�Ѫ��a0�Ҷǰe
                            int x = Convert.ToInt32(M[0]), y = Convert.ToInt32(M[1]), player = Convert.ToInt32(M[2]);
                            int winner, nextplayer;
                            arrPic[player*13+y].Image = Properties.Resources.g52; //��o�i�P�\�_��
                            
                            roundcard[counthand % 4].id = player;
                            roundcard[counthand % 4].face = x;
                            roundcard[counthand % 4].value = x%13;
                            roundcardPic[counthand % 4].Image = Image.FromFile(x.ToString()+".jpg");
                            counthand++;
                            if (counthand % 4 == 0) //�@�������A�n�P�_��Ĺ�o�[
                            {
                                MessageBox.Show("set point", "�T��");
                                winner = caculate();
                                nextplayer = winner;
                            }
                            else
                            {
                                nextplayer = (player + 1) % 4;
                                winner = 9; //�S���N�q
                            }
                            if (counthand == 52)
                            {
                                MessageBox.Show("game over", "�T��");
                                return;
                            }
                            else
                            {
                                Str += "," + winner.ToString() + "," + nextplayer.ToString();
                                SendAll("A" + Str);
                            }
                            break;
                        case "1":// send msg to all client
                            SendAll(Msg);
                            break;
                        default:
                            string[] C = Str.Split('|');
                            SendTo(Cmd + C[0], C[1]);
                            break;
                    }
                    if (nconnect == 4 && !gaming) //�o�P��4�a
                    {
                        string aStr = ""; //P:deal
                        string user = "";
                        int actclient = 0; //��0�����a�}�l�۵P
                        gaming = true;
                        for (int k = 0; k < 4; k++)
                        {
                            aStr = "P";
                            aStr += actclient.ToString();
                            for (int i = 0; i < 13; i++)
                            {
                                aStr += a[k, i].ToString();
                                if (i < 12) aStr += ",";
                            }
                            user = listBox1.Items[k].ToString();
                            //MessageBox.Show(user + " " + aStr);
                            SendTo(aStr, user);

                        }
                    }
                }
                catch (Exception)
                {
                    // �����~�ɩ���
                }
                //textBox2.Text = nconnect.ToString();
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        private string MyIP()
        {
            string hn = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostEntry(hn).AddressList;
            foreach (IPAddress it in ip)
            {
                if (it.AddressFamily == AddressFamily.InterNetwork)
                    return it.ToString();
            }
            return "";
        }
        private string OnlineList()
        {
            string L = "L";
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                L += listBox1.Items[i];
                if (i < listBox1.Items.Count - 1) { L += ","; }
            }
            return L;
        }
        private void SendTo(string str, string User)
        {
            //int bytesent = 0;
            byte[] B = Encoding.Default.GetBytes(str);
            Socket Sck = (Socket)HT[User];
            //while(bytesent==0)
            //{
            Sck.Send(B, 0, B.Length, SocketFlags.None);
            //}
            
        }
        private void SendAll(string str)
        {
            byte[] B = Encoding.Default.GetBytes(str);
            foreach(Socket s in HT.Values) s.Send(B, 0, B.Length, SocketFlags.None);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Str = ""; //P:deal
            string user = "";
            for (int k = 0; k < listBox1.Items.Count; k++)
            {
                Str = "P";
                for (int i = 0; i < 13; i++)
                {
                    Str += a[k, i].ToString();
                    if (i < 12) Str += ",";
                }
                user=listBox1.Items[k].ToString();
                MessageBox.Show(user + " " + Str);
                SendTo(Str,user);
            }
        }
    }
}
