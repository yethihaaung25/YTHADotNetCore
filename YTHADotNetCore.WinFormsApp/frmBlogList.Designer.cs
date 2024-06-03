namespace YTHADotNetCore.WinFormsApp
{
    partial class frmBlogList
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
            dgvBlogList = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvBlogList).BeginInit();
            SuspendLayout();
            // 
            // dgvBlogList
            // 
            dgvBlogList.AllowUserToAddRows = false;
            dgvBlogList.AllowUserToDeleteRows = false;
            dgvBlogList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBlogList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBlogList.Columns.AddRange(new DataGridViewColumn[] { colId, colEdit, colDelete, colTitle, colAuthor, colContent });
            dgvBlogList.Dock = DockStyle.Fill;
            dgvBlogList.Location = new Point(0, 0);
            dgvBlogList.Name = "dgvBlogList";
            dgvBlogList.ReadOnly = true;
            dgvBlogList.RowHeadersWidth = 51;
            dgvBlogList.RowTemplate.Height = 29;
            dgvBlogList.Size = new Size(978, 544);
            dgvBlogList.TabIndex = 0;
            dgvBlogList.CellContentClick += dgvBlogList_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "BlogId";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.HeaderText = "Edit";
            colEdit.MinimumWidth = 6;
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Text = "Edit";
            colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "Delete";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "BlogTitle";
            colTitle.HeaderText = "Title";
            colTitle.MinimumWidth = 6;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "BlogAuthor";
            colAuthor.HeaderText = "Author";
            colAuthor.MinimumWidth = 6;
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "BlogContent";
            colContent.HeaderText = "Content";
            colContent.MinimumWidth = 6;
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // frmBlogList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 544);
            Controls.Add(dgvBlogList);
            Name = "frmBlogList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BlogList";
            Load += frmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBlogList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvBlogList;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colContent;
    }
}