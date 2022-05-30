using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace notepad
{
    public partial class Form1 : Form
    {
        private bool isModified;
        private string currentFileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            //richTextBox1 을 초기화 (Clear) 한다
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 파일 열기
            OpenFileDialog op = new OpenFileDialog();
            // OpenFileDialog 를 op 변수를 만들어서 사용 가능하도록한다 이하 같은 구문은 전부 같은 내용으로 보면 됨
            if (op.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.LoadFile(op.FileName, RichTextBoxStreamType.PlainText);
                this.Text = op.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            // 파일 열기에서 설명
            sv.Filter = "텍스트 문서(*.txt)|*.txt|All Files (*.*)|";
            // 저장 파일 종류 설정
            if (sv.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SaveFile(sv.FileName, RichTextBoxStreamType.PlainText);
                this.Text = sv.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {// 종료
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {// 잘라내기
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {//복사
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {//붙어 놓기
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {//뒤로가기(되돌기기)
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {//앞으로 가기
            richTextBox1.Redo();
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {//폰트 설정
            FontDialog fd = new FontDialog();
            fd.Font = richTextBox1.SelectionFont;
            if (fd.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {//배경색 설정
            ColorDialog cr = new ColorDialog();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = cr.Color;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //변경되고 난 후에 아직 저장X -> 닫기 시도
            if (this.isModified)
            {
                string msg = string.Format("{0} 파일의 내용이 변경되었습니다.\r\n\r\n변경된 내용을 저장하시겠습니까?",
                Path.GetFileName(this.currentFileName));

                DialogResult result = MessageBox.Show(msg, "메모장", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    //저장 메뉴 실행과 동일
                    saveToolStripMenuItem_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    //취소 -> 폼닫기 취소
                    e.Cancel = true;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        { // 도움말 의 정보 메시지 박스 설정
            MessageBox.Show("객체지향프로그래밍 과제");
        }

    }
}
