namespace HttpMessageBus.EXE_Client.WF
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonClientConnect = new System.Windows.Forms.Button();
			this.textBoxHttpMessageBusServer = new System.Windows.Forms.TextBox();
			this.labelHttpMessageBusServer = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1Message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonClientConnect
			// 
			this.buttonClientConnect.Location = new System.Drawing.Point(12, 63);
			this.buttonClientConnect.Name = "buttonClientConnect";
			this.buttonClientConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonClientConnect.TabIndex = 13;
			this.buttonClientConnect.Text = "Connect";
			this.buttonClientConnect.UseVisualStyleBackColor = true;
			// 
			// textBoxHttpMessageBusServer
			// 
			this.textBoxHttpMessageBusServer.Location = new System.Drawing.Point(163, 6);
			this.textBoxHttpMessageBusServer.Name = "textBoxHttpMessageBusServer";
			this.textBoxHttpMessageBusServer.Size = new System.Drawing.Size(585, 20);
			this.textBoxHttpMessageBusServer.TabIndex = 12;
			// 
			// labelHttpMessageBusServer
			// 
			this.labelHttpMessageBusServer.AutoSize = true;
			this.labelHttpMessageBusServer.Location = new System.Drawing.Point(12, 9);
			this.labelHttpMessageBusServer.Name = "labelHttpMessageBusServer";
			this.labelHttpMessageBusServer.Size = new System.Drawing.Size(131, 13);
			this.labelHttpMessageBusServer.TabIndex = 11;
			this.labelHttpMessageBusServer.Text = "Http Message Bus Server:";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(163, 31);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(585, 20);
			this.textBox2.TabIndex = 15;
			// 
			// label1Message
			// 
			this.label1Message.AutoSize = true;
			this.label1Message.Location = new System.Drawing.Point(12, 34);
			this.label1Message.Name = "label1Message";
			this.label1Message.Size = new System.Drawing.Size(53, 13);
			this.label1Message.TabIndex = 14;
			this.label1Message.Text = "Message:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(762, 99);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label1Message);
			this.Controls.Add(this.buttonClientConnect);
			this.Controls.Add(this.textBoxHttpMessageBusServer);
			this.Controls.Add(this.labelHttpMessageBusServer);
			this.Name = "Form1";
			this.Text = "Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonClientConnect;
		private System.Windows.Forms.TextBox textBoxHttpMessageBusServer;
		private System.Windows.Forms.Label labelHttpMessageBusServer;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label1Message;


	}
}

