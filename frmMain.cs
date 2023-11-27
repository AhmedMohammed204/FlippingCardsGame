using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlippingCardsGame
{
    public partial class frmMain : Form
    {
        stGameStatus GameStatus;
        struct stGameStatus
        {
            public Button UserChoice1;
            public Button UserChoice2;
            public short Rounds;
            public bool ReflipPrevCards;
        }
        Random rd = new Random();

        void Swap(Button btn1, Button btn2)
        {
            Button Temp = new Button();
            Temp.Tag = btn1.Tag;
            btn1.Tag = btn2.Tag;
            btn2.Tag = Temp.Tag;
        }
        void SwapWithRandomButton(Button btn, byte btn2Index)
        {
            switch (btn2Index)
            {
                case 1:
                    Swap(btn, button1);
                    break;
                case 2:
                    Swap(btn, button2);
                    break;
                case 3:
                    Swap(btn, button3);
                    break;
                case 4:
                    Swap(btn, button4);
                    break;
                case 5:
                    Swap(btn, button5);
                    break;

                case 6:
                    Swap(btn, button6);
                    break;

                case 7:
                    Swap(btn, button7);
                    break;

                case 8:
                    Swap(btn, button8);
                    break;

                case 9:
                    Swap(btn, button9);
                    break;

                case 10:
                    Swap(btn, button10);
                    break;
                case 11:
                    Swap(btn, button11);
                    break;
                case 12:
                    Swap(btn, button12);
                    break;


            }
        }
        void MixAllButtons()
        {
            SwapWithRandomButton(button1, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button2, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button3, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button4, (byte)rd.Next(1, 25));

            SwapWithRandomButton(button5, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button6, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button7, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button8, (byte)rd.Next(1, 25));

            SwapWithRandomButton(button9, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button10, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button11, (byte)rd.Next(1, 25));
            SwapWithRandomButton(button12, (byte)rd.Next(1, 25));
        }
        void ResetButton(Button btn)
        {
            btn.Text = "?";
            btn.Enabled = true;
            btn.BackColor = Color.LightGreen;
        }
        public frmMain()
        {
            InitializeComponent();
        }  
        void GenerateCard(Button btn1, Button btn2)
        {
            string RandomNum = rd.Next(0, 100).ToString();
            btn1.Tag = RandomNum;
            ResetButton(btn1);
            btn2.Tag = RandomNum;
            ResetButton(btn2);
        }
        void GenerateCards()
        {
            if(lblChoice1.Text == "?" && lblChoice2.Text == "?")
            {
                MessageBox.Show("Sorry, you can't start new round wthout flip any card ):", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GameStatus.Rounds++;
            lblRounds.Text = GameStatus.Rounds.ToString();
            lblChoice1.Text = "?";
            lblChoice2.Text = "?";

            GenerateCard(button1, button2);
            GenerateCard(button3, button4);
            GenerateCard(button5, button6);

            GenerateCard(button7, button8);
            GenerateCard(button9, button10);
            GenerateCard(button11, button12);

            MixAllButtons();
        }
        void StartGame()
        {
            GameStatus.UserChoice1 = new Button();
            GameStatus.UserChoice1 = new Button();
            GameStatus.ReflipPrevCards = false;
            GenerateCards();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GameStatus.Rounds = 0;
            StartGame();
        }
        bool IsAllCardFliped()
        {
            return (
                button1.Enabled == false &&
                button2.Enabled == false &&
                button3.Enabled == false &&
                button4.Enabled == false &&
                button5.Enabled == false &&
                button6.Enabled == false &&
                button7.Enabled == false &&
                button8.Enabled == false &&
                button9.Enabled == false &&
                button10.Enabled == false &&
                button11.Enabled == false &&
                button12.Enabled == false

                );
        }
        void CheckAnswer()
        {
            
            if(!(GameStatus.UserChoice1.Tag == GameStatus.UserChoice2.Tag))
            {
                //MessageBox.Show("Wrong Answer ):", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                
                GameStatus.ReflipPrevCards = true;


                return;
            }


            GameStatus.UserChoice1 = new Button();
            GameStatus.UserChoice2 = new Button();

            if (IsAllCardFliped())
            {

                MessageBox.Show("Great!\nYou have finished the game\nYou can start again by using \"Generate\" button", "Don!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void CheckUserChoice(Button btnClicked)
        {
            switch(GameStatus.UserChoice1.Tag)
            {
                case null:
                    GameStatus.UserChoice1 = btnClicked;
                    lblChoice1.Text = btnClicked.Tag.ToString();

                    lblChoice2.Text = "?";
                    break;
                default:
                    GameStatus.UserChoice2 = btnClicked;
                    lblChoice2.Text = btnClicked.Tag.ToString();

                    CheckAnswer();
                    break;
            }
        }

        void ReflipCards()
        {
            if(GameStatus.ReflipPrevCards)
            {
                ResetButton(GameStatus.UserChoice1);
                ResetButton(GameStatus.UserChoice2);
                GameStatus.UserChoice1 = new Button();
                GameStatus.UserChoice2 = new Button();
                GameStatus.ReflipPrevCards = false;
            }
        }
        private void btnClick(object sender, EventArgs e)
        {
            Button btnClicked = (Button)sender;

            btnClicked.Enabled = false;
            btnClicked.Text = btnClicked.Tag.ToString();
            btnClicked.BackColor = Color.Gray;
            ReflipCards();
            CheckUserChoice(btnClicked);

        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
