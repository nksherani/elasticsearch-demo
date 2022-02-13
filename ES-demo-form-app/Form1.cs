namespace ES_demo_form_app
{
    public partial class Form1 : Form
    {
        ESWrapper eSWrapper;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eSWrapper = new ESWrapper(txtDefaultIndex.Text);
            OutputToConsole("Initialization", null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eSWrapper = new ESWrapper(txtDefaultIndex.Text);
            OutputToConsole("Initialization", null);
        }

        private void createIndex_Click(object sender, EventArgs e)
        {
            eSWrapper.CreateIndex(txtDefaultIndex.Text);
            var response = eSWrapper.DeleteIndex(txtDefaultIndex.Text);
            OutputToConsole("Create Index", response.OriginalException);
        }

        private void deleteIndex_Click(object sender, EventArgs e)
        {
            var response = eSWrapper.DeleteIndex(txtDefaultIndex.Text);
            OutputToConsole("Delete Index", response.OriginalException);

        }

        private void match_Click(object sender, EventArgs e)
        {
            var students = eSWrapper.SearchMatchQuery(txtSearch.Text);
            OutputToConsole(students);
        }

        private void matchPhrase_Click(object sender, EventArgs e)
        {
            var students = eSWrapper.SearchMatchPhraseQuery(txtSearch.Text);
            OutputToConsole(students);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var students = eSWrapper.SearchTermQuery(txtSearch.Text);
            OutputToConsole(students);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var response = eSWrapper.CreateStudent(1, "Naveed Ahmed Khan");
            OutputToConsole("Insert", response.OriginalException);
            response = eSWrapper.CreateStudent(2, "naveed Khan");
            OutputToConsole("Insert", response.OriginalException);
            response = eSWrapper.CreateStudent(2, "naveed Khan Ahmed");
            OutputToConsole("Insert", response.OriginalException);
            response = eSWrapper.CreateStudent(3, "Athar Adeel");
            OutputToConsole("Insert", response.OriginalException);
            response = eSWrapper.CreateStudent(4, "Orhaan Bin Naveed");
            OutputToConsole("Insert", response.OriginalException);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            var response = await eSWrapper.DeleteAllStudents();
            OutputToConsole("Delete", response.OriginalException);
        }

        private void OutputToConsole(string action, Exception originalException)
        {
            if (originalException != null)
            {
                richTextBox1.AppendText("\r\n" + originalException.Message);
            }
            else
            {
                richTextBox1.AppendText($"\r\n {action} successfull");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var students = eSWrapper.SearchMatchAll(txtSearch.Text);
            OutputToConsole(students);
        }
        private void OutputToConsole(IEnumerable<Student> students)
        {
            richTextBox1.Text = "";
            foreach (var student in students)
            {
                richTextBox1.AppendText($"\r\n{student.StudentId} - {student.Content}");
            }
        }
    }
}