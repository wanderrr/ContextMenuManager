﻿using BulePointLilac.Controls;
using BulePointLilac.Methods;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContextMenuManager.Controls
{
    class NewItemForm : ResizbleForm
    {
        public NewItemForm()
        {
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            this.Font = SystemFonts.MenuFont;
            this.MaximizeBox = this.MinimizeBox = false;
            this.ShowIcon = this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.VerticalResizable = false;
            InitializeComponents();
        }

        public string ItemText { get => txtText.Text; set => txtText.Text = value; }
        public string Command { get => txtCommand.Text; set => txtCommand.Text = value; }
        public string Arguments { get => txtArguments.Text; set => txtArguments.Text = value; }
        public string FullCommand
        {
            get
            {
                if(Arguments.IsNullOrWhiteSpace()) return Command;
                else if(Command.IsNullOrWhiteSpace()) return Arguments;
                else return $"\"{Command}\" \"{Arguments}\"";
            }
        }

        protected readonly Label lblText = new Label
        {
            Text = AppString.Dialog.ItemText,
            AutoSize = true
        };
        protected readonly Label lblCommand = new Label
        {
            Text = AppString.Dialog.ItemCommand,
            AutoSize = true
        };
        protected readonly Label lblArguments = new Label
        {
            Text = AppString.Dialog.CommandArguments,
            AutoSize = true
        };
        protected readonly TextBox txtText = new TextBox();
        protected readonly TextBox txtCommand = new TextBox();
        protected readonly TextBox txtArguments = new TextBox();
        protected readonly Button btnBrowse = new Button
        {
            Text = AppString.Dialog.Browse,
            AutoSize = true
        };
        protected readonly Button btnOk = new Button
        {
            Text = AppString.Dialog.Ok,
            AutoSize = true
        };
        protected readonly Button btnCancel = new Button
        {
            DialogResult = DialogResult.Cancel,
            Text = AppString.Dialog.Cancel,
            AutoSize = true
        };

        private static Size LastSize = new Size();

        protected virtual void InitializeComponents()
        {
            this.Controls.AddRange(new Control[] { lblText, lblCommand, lblArguments,
                txtText, txtCommand, txtArguments, btnBrowse, btnOk, btnCancel });
            int a = 20.DpiZoom();
            btnBrowse.Anchor = btnOk.Anchor = btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            txtText.Top = lblText.Top = lblText.Left = lblCommand.Left = lblArguments.Left = a;
            btnBrowse.Top = txtCommand.Top = lblCommand.Top = txtText.Bottom + a;
            lblArguments.Top = txtArguments.Top = txtCommand.Bottom + a;
            btnOk.Top = btnCancel.Top = txtArguments.Bottom + a;
            btnCancel.Left = btnBrowse.Left = this.ClientSize.Width - btnCancel.Width - a;
            btnOk.Left = btnCancel.Left - btnOk.Width - a;
            int b = Math.Max(Math.Max(lblText.Width, lblCommand.Width), lblArguments.Width) + btnBrowse.Width + 4 * a;
            this.ClientSize = new Size(250.DpiZoom() + b, btnOk.Bottom + a);
            this.MinimumSize = this.Size;
            this.Resize += (sender, e) =>
            {
                txtText.Width = txtCommand.Width = txtArguments.Width = this.ClientSize.Width - b;
                txtText.Left = txtCommand.Left = txtArguments.Left = btnBrowse.Left - txtCommand.Width - a;
                LastSize = this.Size;
            };
            if(LastSize != null) this.Size = LastSize;
            this.OnResize(null);
        }
    }
}