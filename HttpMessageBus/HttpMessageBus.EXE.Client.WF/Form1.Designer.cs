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
			this.buttonUnsubscribe = new System.Windows.Forms.Button();
			this.textBoxHostIPAddress = new System.Windows.Forms.TextBox();
			this.labelHostIPAddress = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.labelPort = new System.Windows.Forms.Label();
			this.textBoxChannel = new System.Windows.Forms.TextBox();
			this.labelChannel = new System.Windows.Forms.Label();
			this.buttonSubscribe = new System.Windows.Forms.Button();
			this.labelMessage = new System.Windows.Forms.Label();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.labelMessages = new System.Windows.Forms.Label();
			this.textBoxMessages = new System.Windows.Forms.TextBox();
			this.buttonNotify = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonUnsubscribe
			// 
			this.buttonUnsubscribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUnsubscribe.Location = new System.Drawing.Point(253, 86);
			this.buttonUnsubscribe.Name = "buttonUnsubscribe";
			this.buttonUnsubscribe.Size = new System.Drawing.Size(75, 23);
			this.buttonUnsubscribe.TabIndex = 13;
			this.buttonUnsubscribe.Text = "Unsubscribe";
			this.buttonUnsubscribe.UseVisualStyleBackColor = true;
			// 
			// textBoxHostIPAddress
			// 
			this.textBoxHostIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxHostIPAddress.Location = new System.Drawing.Point(110, 2);
			this.textBoxHostIPAddress.Name = "textBoxHostIPAddress";
			this.textBoxHostIPAddress.Size = new System.Drawing.Size(218, 20);
			this.textBoxHostIPAddress.TabIndex = 12;
			// 
			// labelHostIPAddress
			// 
			this.labelHostIPAddress.AutoSize = true;
			this.labelHostIPAddress.Location = new System.Drawing.Point(12, 9);
			this.labelHostIPAddress.Name = "labelHostIPAddress";
			this.labelHostIPAddress.Size = new System.Drawing.Size(80, 13);
			this.labelHostIPAddress.TabIndex = 11;
			this.labelHostIPAddress.Text = "HostIPAddress:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPort.Location = new System.Drawing.Point(110, 27);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(218, 20);
			this.textBoxPort.TabIndex = 15;
			// 
			// labelPort
			// 
			this.labelPort.AutoSize = true;
			this.labelPort.Location = new System.Drawing.Point(12, 34);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(29, 13);
			this.labelPort.TabIndex = 14;
			this.labelPort.Text = "Port:";
			// 
			// textBoxChannel
			// 
			this.textBoxChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxChannel.Location = new System.Drawing.Point(110, 53);
			this.textBoxChannel.Name = "textBoxChannel";
			this.textBoxChannel.Size = new System.Drawing.Size(218, 20);
			this.textBoxChannel.TabIndex = 16;
			// 
			// labelChannel
			// 
			this.labelChannel.AutoSize = true;
			this.labelChannel.Location = new System.Drawing.Point(12, 56);
			this.labelChannel.Name = "labelChannel";
			this.labelChannel.Size = new System.Drawing.Size(49, 13);
			this.labelChannel.TabIndex = 17;
			this.labelChannel.Text = "Channel:";
			// 
			// buttonSubscribe
			// 
			this.buttonSubscribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSubscribe.Location = new System.Drawing.Point(172, 86);
			this.buttonSubscribe.Name = "buttonSubscribe";
			this.buttonSubscribe.Size = new System.Drawing.Size(75, 23);
			this.buttonSubscribe.TabIndex = 18;
			this.buttonSubscribe.Text = "Subscribe";
			this.buttonSubscribe.UseVisualStyleBackColor = true;
			// 
			// labelMessage
			// 
			this.labelMessage.AutoSize = true;
			this.labelMessage.Location = new System.Drawing.Point(12, 118);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(53, 13);
			this.labelMessage.TabIndex = 20;
			this.labelMessage.Text = "Message:";
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessage.Location = new System.Drawing.Point(110, 115);
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(218, 20);
			this.textBoxMessage.TabIndex = 19;
			// 
			// labelMessages
			// 
			this.labelMessages.AutoSize = true;
			this.labelMessages.Location = new System.Drawing.Point(12, 176);
			this.labelMessages.Name = "labelMessages";
			this.labelMessages.Size = new System.Drawing.Size(58, 13);
			this.labelMessages.TabIndex = 22;
			this.labelMessages.Text = "Messages:";
			// 
			// textBoxMessages
			// 
			this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessages.Location = new System.Drawing.Point(110, 173);
			this.textBoxMessages.Multiline = true;
			this.textBoxMessages.Name = "textBoxMessages";
			this.textBoxMessages.Size = new System.Drawing.Size(218, 114);
			this.textBoxMessages.TabIndex = 21;
			// 
			// buttonNotify
			// 
			this.buttonNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNotify.Location = new System.Drawing.Point(253, 141);
			this.buttonNotify.Name = "buttonNotify";
			this.buttonNotify.Size = new System.Drawing.Size(75, 23);
			this.buttonNotify.TabIndex = 23;
			this.buttonNotify.Text = "Notify";
			this.buttonNotify.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 299);
			this.Controls.Add(this.buttonNotify);
			this.Controls.Add(this.labelMessages);
			this.Controls.Add(this.textBoxMessages);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.buttonSubscribe);
			this.Controls.Add(this.labelChannel);
			this.Controls.Add(this.textBoxChannel);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.labelPort);
			this.Controls.Add(this.buttonUnsubscribe);
			this.Controls.Add(this.textBoxHostIPAddress);
			this.Controls.Add(this.labelHostIPAddress);
			this.Name = "Form1";
			this.Text = "Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonUnsubscribe;
		private System.Windows.Forms.TextBox textBoxHostIPAddress;
		private System.Windows.Forms.Label labelHostIPAddress;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.TextBox textBoxChannel;
		private System.Windows.Forms.Label labelChannel;
		private System.Windows.Forms.Button buttonSubscribe;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label labelMessages;
		private System.Windows.Forms.TextBox textBoxMessages;
		private System.Windows.Forms.Button buttonNotify;


	}
}

