using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KXTapiAPI;
namespace Bel3
{
    public partial class Telefonate : Form
    {
        string[] start;
        int[] id;
        private ListView call;
        private ColumnHeader columnHeader1;
        string[] end;
        public Telefonate()
        {
            InitializeComponent();
            Timer t = new Timer();
            start = new string[150];
            end = new string[150];
            id = new int[150];
           
            try
            {
                //SDK.
 SDK.KXMessageEvent += new SDK.KXMessage(onSDKevent);
                SDK.Initialize();
               
            }
            catch (ClsException ex)
            {

            }
        }



        private void Telefonate_Load(object sender, EventArgs e)
        {
           
        }

        uint lineid, callid;
        V_CALLINFO callinfo;
        V_LINEINFO lineinfo;
        int count = 0;
        void onSDKevent(V_EVENTMESSAGEINFO e)
        {
            switch (e.e_EventType)
            {             
                case E_EVENTTYPE.KX_CALLSTATE:
                        {
                        lineid = e.uiMsgID;
                        callid = (uint)e.objResult;
                        lineinfo=SDK.GetLineInfo(lineid);
                        callinfo= SDK.GetCallInfo(callid);
                        if (callinfo.e_CallState == E_LINECALLSTATE.KX_CONNECTED)
                        {
                            foreach(int i in id)
                            {
                                if (i == null)
                                {

                                    start[i] = DateTime.Now.ToLongDateString();
                                    call.Items.Add("Telefonata da "+lineinfo.strExtName+" "+lineinfo.strLineName+" "+lineinfo.e_ExtType+" "+lineinfo.e_LineStatus+" "+lineinfo.e_LineType+" "+callinfo.e_CallState+" a "+ callinfo.strCalledID+" "+callinfo.strCalledIDName);
                                    count++;
                                    break;
                                }
                                else
                                {
                                    count++;
                                }
                            }
                          
                        }
                        if (callinfo.e_CallState == E_LINECALLSTATE.KX_DISCONNECTED)
                        {
                            
                        }
                                break;
                        }

                   
            }
        }
        

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                SDK.Shutdown();
            }
            catch (ClsException ex)
            {

            }
            base.OnClosing(e);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Telefonate));
            this.call = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // call
            // 
            this.call.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.call.Location = new System.Drawing.Point(12, 12);
            this.call.Name = "call";
            this.call.Size = new System.Drawing.Size(495, 249);
            this.call.TabIndex = 0;
            this.call.UseCompatibleStateImageBehavior = false;
            this.call.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Chiamate";
            // 
            // Telefonate
            // 
            this.ClientSize = new System.Drawing.Size(519, 273);
            this.Controls.Add(this.call);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Telefonate";
            this.ResumeLayout(false);

        }
    }
}
