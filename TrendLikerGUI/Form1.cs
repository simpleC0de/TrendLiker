using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Referencing everything non-standard below

using System.IO;
using System.Net;
using System.Net.Sockets;


using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Converters;
using InstaSharper.Helpers;
using InstaSharper.Logger;
using InstaSharper.API.Builder;
using InstaSharper.Classes.Android;
using InstaSharper.Classes.Models;
using InstaSharper.Classes.ResponseWrappers;
using InstaSharper.Converters.Json;
using InstaSharper.Classes.Android.DeviceInfo;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading;


namespace TrendLikerGUI
{
    public partial class Form1 : Form
    {

        /* Only this object of the api will be used */
        private InstaApi api;

        private bool loggedIn = false;

        private bool runningProcess = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hideorshowTags(true);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!loggedIn)
                GenerateDevice();
            else
                InstagramLogout();
        }

        private void GenerateDevice()
        {
            AndroidDevice device = AndroidDeviceGenerator.GetRandomAndroidDevice();

            string user = "", password = "";

            user = usernameText.Text;
            password = passwordText.Text;

            ApiRequestMessage _requestMessage = new ApiRequestMessage
            {
                phone_id = device.PhoneGuid.ToString(),
                guid = device.DeviceGuid,
                password = password,
                username = user,
                device_id = AndroidDeviceGenerator.HTC10
            };

            HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://i.instagram.com");

            UserSessionData userConfig = new UserSessionData();

            userConfig.UserName = user;
            userConfig.Password = password;

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            
            /*Add this if you want to add proxy support.*/
              /*if (proxyFound)
              {
                  httpClientHandler.UseProxy = true;
                 IWebProxy proxy = new WebProxy("proxy");
                  httpClientHandler.Proxy = proxy;
              }
              */

            api = new InstaApi(userConfig, null, client, httpClientHandler, _requestMessage, device);

            InstagramLogin();
        }

        private void InstagramLogin()
        {

            AppendTexttoLogging("Logging in with account: " + usernameText.Text + "...");

            new Thread(() =>
            {

                IResult<bool> result = api.Login();

                if (result.Succeeded)
                {
                    AppendTexttoLogging("Logged in successfully!");
                    loggedIn = true;
                    changeButtonText("Logout");
                    hideorshowTags(false);
                }
                else
                {
                    AppendTexttoLogging("Logging in failed! Try again!");
                    loggedIn = false;
                    changeButtonText("Login");
                    hideorshowTags(true);
                }

                Thread.CurrentThread.Abort();

            }).Start();
        }


        private void InstagramLogout()
        {
            new Thread(() =>
            {

                IResult<bool> result = api.Logout();

                if (result.Succeeded)
                {
                    AppendTexttoLogging("Logged out successfully!");
                    loggedIn = false;
                    changeButtonText("Login");
                    hideorshowTags(true);
                }
                else
                {
                    AppendTexttoLogging("Still logged in! Some error occured.");
                    loggedIn = true;
                    changeButtonText("Logout");
                }

            }).Start();
        }

        private void AppendTexttoLogging(string text)
        {

            text = text + "\n";

            if (loggingTextBox.InvokeRequired)
            { 
                loggingTextBox.BeginInvoke(new Action(() =>
                {
                    loggingTextBox.Focus();
                    loggingTextBox.AppendText(text);
                }));
            }
            else
            {
                loggingTextBox.Focus();
                loggingTextBox.AppendText(text);
            }
        }

        private void changeButtonText(string text)
        {
            if (loginButton.InvokeRequired)
            {
                loginButton.BeginInvoke(new Action(() =>
                {
                    loginButton.Text = text;
                }));
            }
            else
            {
                loginButton.Text = text;
            }
        }

        private void startButtonChange(string text)
        {
            if (startButton.InvokeRequired)
            {
                startButton.BeginInvoke(new Action(() =>
                {
                    startButton.Text = text;
                }));
            }
            else
            {
                startButton.Text = text;
            }
        }

        private void hideorshowTags(bool hide)
        {
            if (hide)
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.BeginInvoke(new Action(() =>
                    {
                        textBox1.Visible = false;
                    }));
                }
                if (textBox2.InvokeRequired)
                {
                    textBox2.BeginInvoke(new Action(() =>
                    {
                        textBox2.Visible = false;
                    }));
                }
                if (textBox3.InvokeRequired)
                {
                    textBox3.BeginInvoke(new Action(() =>
                    {
                        textBox3.Visible = false;
                    }));
                }
                if (textBox4.InvokeRequired)
                {
                    textBox4.BeginInvoke(new Action(() =>
                    {
                        textBox4.Visible = false;
                    }));
                }
                if (textBox5.InvokeRequired)
                {
                    textBox5.BeginInvoke(new Action(() =>
                    {
                        textBox5.Visible = false;
                    }));
                }
                if (textBox6.InvokeRequired)
                {
                    textBox6.BeginInvoke(new Action(() =>
                    {
                        textBox6.Visible = false;
                    }));
                }
                if (textBox7.InvokeRequired)
                {
                    textBox7.BeginInvoke(new Action(() =>
                    {
                        textBox7.Visible = false;
                    }));
                }
                if (textBox8.InvokeRequired)
                {
                    textBox8.BeginInvoke(new Action(() =>
                    {
                        textBox8.Visible = false;
                    }));
                }
                if (textBox9.InvokeRequired)
                {
                    textBox9.BeginInvoke(new Action(() =>
                    {
                        textBox9.Visible = false;
                    }));
                }
                if (textBox10.InvokeRequired)
                {
                    textBox10.BeginInvoke(new Action(() =>
                    {
                        textBox10.Visible = false;
                    }));
                }
                if (textBox11.InvokeRequired)
                {
                    textBox11.BeginInvoke(new Action(() =>
                    {
                        textBox11.Visible = false;
                    }));
                }

                if (startButton.InvokeRequired)
                {
                    startButton.BeginInvoke(new Action(() =>
                    {
                        startButton.Visible = false;
                    }));
                }


                if (textBox12.InvokeRequired)
                {
                    textBox12.BeginInvoke(new Action(() =>
                    {
                        textBox12.Visible = false;
                    }));
                }
                else
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                    textBox8.Visible = false;
                    textBox9.Visible = false;
                    textBox10.Visible = false;
                    textBox11.Visible = false;
                    textBox12.Visible = false;

                    startButton.Visible = true;
                }
            }
            else
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.BeginInvoke(new Action(() =>
                    {
                        textBox1.Visible = true;
                    }));
                }
                if (textBox2.InvokeRequired)
                {
                    textBox2.BeginInvoke(new Action(() =>
                    {
                        textBox2.Visible = true;
                    }));
                }
                if (textBox3.InvokeRequired)
                {
                    textBox3.BeginInvoke(new Action(() =>
                    {
                        textBox3.Visible = true;
                    }));
                }
                if (textBox4.InvokeRequired)
                {
                    textBox4.BeginInvoke(new Action(() =>
                    {
                        textBox4.Visible = true;
                    }));
                }
                if (textBox5.InvokeRequired)
                {
                    textBox5.BeginInvoke(new Action(() =>
                    {
                        textBox5.Visible = true;
                    }));
                }
                if (textBox6.InvokeRequired)
                {
                    textBox6.BeginInvoke(new Action(() =>
                    {
                        textBox6.Visible = true;
                    }));
                }
                if (textBox7.InvokeRequired)
                {
                    textBox7.BeginInvoke(new Action(() =>
                    {
                        textBox7.Visible = true;
                    }));
                }
                if (textBox8.InvokeRequired)
                {
                    textBox8.BeginInvoke(new Action(() =>
                    {
                        textBox8.Visible = true;
                    }));
                }
                if (textBox9.InvokeRequired)
                {
                    textBox9.BeginInvoke(new Action(() =>
                    {
                        textBox9.Visible = true;
                    }));
                }
                if (textBox10.InvokeRequired)
                {
                    textBox10.BeginInvoke(new Action(() =>
                    {
                        textBox10.Visible = true;
                    }));
                }
                if (textBox11.InvokeRequired)
                {
                    textBox11.BeginInvoke(new Action(() =>
                    {
                        textBox11.Visible = true;
                    }));
                }

                if (startButton.InvokeRequired)
                {
                    startButton.BeginInvoke(new Action(() =>
                    {
                        startButton.Visible = true;
                    }));
                }

                if (textBox12.InvokeRequired)
                {
                    textBox12.BeginInvoke(new Action(() =>
                    {
                        textBox12.Visible = true;
                    }));
                }
                else
                {
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    textBox5.Visible = true;
                    textBox6.Visible = true;
                    textBox7.Visible = true;
                    textBox8.Visible = true;
                    textBox9.Visible = true;
                    textBox10.Visible = true;
                    textBox11.Visible = true;
                    textBox12.Visible = true;

                    startButton.Visible = true;
                }
            }

          

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            List<string> tags = new List<string>();

            if (!loggedIn)
            {
                MessageBox.Show("You have to login first!");
                return;
            }
            if (!runningProcess)
            {
                /* Gathering tags */

                if (textBox1.Text.Length != 0 && textBox1.Text.ToLower() != "enter your tag")
                    tags.Add(textBox1.Text);
                if (textBox2.Text.Length != 0 && textBox2.Text.ToLower() != "enter your tag")
                    tags.Add(textBox2.Text);
                if (textBox3.Text.Length != 0 && textBox3.Text.ToLower() != "enter your tag")
                    tags.Add(textBox3.Text);
                if (textBox4.Text.Length != 0 && textBox4.Text.ToLower() != "enter your tag")
                    tags.Add(textBox4.Text);
                if (textBox5.Text.Length != 0 && textBox5.Text.ToLower() != "enter your tag")
                    tags.Add(textBox5.Text);
                if (textBox6.Text.Length != 0 && textBox6.Text.ToLower() != "enter your tag")
                    tags.Add(textBox6.Text);
                if (textBox7.Text.Length != 0 && textBox7.Text.ToLower() != "enter your tag")
                    tags.Add(textBox7.Text);
                if (textBox8.Text.Length != 0 && textBox8.Text.ToLower() != "enter your tag")
                    tags.Add(textBox8.Text);
                if (textBox9.Text.Length != 0 && textBox9.Text.ToLower() != "enter your tag")
                    tags.Add(textBox9.Text);
                if (textBox10.Text.Length != 0 && textBox10.Text.ToLower() != "enter your tag")
                    tags.Add(textBox10.Text);
                if (textBox11.Text.Length != 0 && textBox11.Text.ToLower() != "enter your tag")
                    tags.Add(textBox11.Text);
                if (textBox12.Text.Length != 0 && textBox12.Text.ToLower() != "enter your tag")
                    tags.Add(textBox12.Text);
            }
            else
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                this.Close();
            }

            if(tags.Count == 0)
            {
                MessageBox.Show("Please enter at least 1 Tag!");
                return;
            }
            startButtonChange("Stop");
            int delay = 13000;
            Random r = new Random();

           
            new Thread(() =>
            {
                runningProcess = true;

                string usedTag = tags[r.Next(tags.Count)];

                AppendTexttoLogging("Using tag: " + usedTag);

                int pageIndex = 2;

                List<InstaMedia> images = api.GetTagFeed(usedTag, pageIndex).Value.Medias;

                AppendTexttoLogging("Liking " + images.Count + " images until using next tag!");
                AppendTexttoLogging("");

                for (int i = 0; i < images.Count; i++)
                {

                    Thread.Sleep(delay + r.Next(3000));

                    string mediaId = images[i].InstaIdentifier;
                    AppendTexttoLogging("Liking media with ID: " + mediaId);
                    AppendTexttoLogging("Posted by: " + images[i].User.UserName);
                    AppendTexttoLogging("Taken on: " + images[i].DeviceTimeStap);
                    api.LikeMedia(mediaId);
                    AppendTexttoLogging("Liked media! #" + i);
                    AppendTexttoLogging("");

                }
                    


            }).Start();


        }
    }
}
