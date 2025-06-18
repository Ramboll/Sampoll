namespace Sampøll;

public partial class Form1 : Form
{
    private static int margin = 10;
    private static int headerHeight = 50;
    private static int rowHeight = 30;
    private static int labelWidth = 100;
    private static int labelHeight = 20;
    private static int inputWidth = 200;
    private static int inputHeight = 20;
    
    private TableLayoutPanel table;
    private DateTimePicker singleDatePicker;
    private Label dateRangeFromLabel, dateRangeToLabel;
    private DateTimePicker dateRangeFromPicker, dateRangeToPicker;
    private CheckBox useDateRangeCheckbox;
    
    public Form1()
    {        
        addHeader();

        addTableLayout();
        
        addProjectNumberField();
        
        addLocationField();
        
        addDateField();

        addInitialsFields();
        
        addSubmitButton();
        
        InitializeComponent();
        
        this.Text = "SAMPØLL";
        int viewWidth = (margin * 2) * 2 + labelWidth + inputWidth;
        int viewHeight = (margin * 2) + (rowHeight * table.RowCount) + headerHeight;
        this.Size = new System.Drawing.Size(viewWidth, viewHeight);
        this.Icon = new Icon("icon.ico");
    }

    private void addHeader()
    {
        Label header = new Label();
        header.Text = "SAMPØLL";
        header.Location = new Point(margin, margin);
        header.Font = new Font("MonoSpace", 24, FontStyle.Bold);
        header.ForeColor = Color.White;
        header.Size = new Size(300, headerHeight);
        header.TextAlign = ContentAlignment.MiddleCenter;
        header.BackColor = Color.DodgerBlue;

        this.Controls.Add(header);
    }

    private void addTableLayout()
    {
        table = new TableLayoutPanel();
        table.ColumnCount = 2;
        table.RowCount = 0; // will grow as you add rows
        table.Location = new Point(margin, margin + headerHeight); // some margin below header
        table.AutoSize = true;
        table.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        table.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, labelWidth));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        this.Controls.Add(table);
    }
    
    private void AddRow(Control label, Control input)
    {
        table.RowCount += 1;
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
        table.Controls.Add(label, 0, table.RowCount - 1);
        table.Controls.Add(input, 1, table.RowCount - 1);
    }
    
    private void addProjectNumberField()
    {        
        Label prjnum_l = new Label();
        prjnum_l.Text = "Project Number:";
        prjnum_l.Size = new Size(labelWidth, labelHeight);
        this.Controls.Add(prjnum_l);
        
        TextBox prjnum_i = new TextBox();
        prjnum_i.Size = new Size(inputWidth, inputHeight);
        this.Controls.Add(prjnum_i);
        
        AddRow(prjnum_l, prjnum_i);
    }
    
    private void addLocationField()
    {
        Label location_l = new Label();
        location_l.Text = "Location:";
        location_l.Size = new Size(labelWidth, labelHeight);
        this.Controls.Add(location_l);
        
        TextBox location_i = new TextBox();
        location_i.Size = new Size(inputWidth, inputHeight);
        this.Controls.Add(location_i);
        
        AddRow(location_l, location_i);
    }
    
    private void addDateField()
{
    Label dateLabel = new Label { Text = "Date:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true };

    singleDatePicker = new DateTimePicker();

    dateRangeFromLabel = new Label { Text = "From:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Visible = false };
    dateRangeFromPicker = new DateTimePicker { Visible = false };

    dateRangeToLabel = new Label { Text = "To:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Visible = false };
    dateRangeToPicker = new DateTimePicker { Visible = false };

    useDateRangeCheckbox = new CheckBox { Text = "Date Range" };
    useDateRangeCheckbox.CheckedChanged += UseDateRangeCheckbox_CheckedChanged;
    
    var dateContainer = new FlowLayoutPanel();
    dateContainer.AutoSize = true;
    dateContainer.Controls.Add(singleDatePicker);
    dateContainer.Controls.Add(dateRangeFromLabel);
    dateContainer.Controls.Add(dateRangeFromPicker);
    dateContainer.Controls.Add(dateRangeToLabel);
    dateContainer.Controls.Add(dateRangeToPicker);

    // Add a new row to the table
    table.RowCount += 1;
    table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

    table.Controls.Add(dateLabel, 0, table.RowCount - 1);
    table.Controls.Add(dateContainer, 1, table.RowCount - 1);

    // Add checkbox on next row spanning both columns (optional)
    table.RowCount += 1;
    table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    table.Controls.Add(useDateRangeCheckbox, 0, table.RowCount - 1);
    table.SetColumnSpan(useDateRangeCheckbox, 2);
}

private void UseDateRangeCheckbox_CheckedChanged(object? sender, EventArgs e)
{
    bool useRange = useDateRangeCheckbox.Checked;

    singleDatePicker.Visible = !useRange;

    dateRangeFromLabel.Visible = useRange;
    dateRangeFromPicker.Visible = useRange;
    dateRangeToLabel.Visible = useRange;
    dateRangeToPicker.Visible = useRange;
}
    
    public void addInitialsFields()
    {
        Label initials_l = new Label();
        initials_l.Text = "Initials:";
        initials_l.Size = new Size(labelWidth, labelHeight);
        this.Controls.Add(initials_l);
        
        TextBox initials_i = new TextBox();
        initials_i.Size = new Size(inputWidth, inputHeight);
        this.Controls.Add(initials_i);

        AddRow(initials_l, initials_i);
    }

    public void addSubmitButton()
    {
        Button submitButton = new Button
        {
            Text = "Submit",
            AutoSize = true,
            Anchor = AnchorStyles.Right
        };

        submitButton.Click += (sender, e) =>
        {
            MessageBox.Show("Form submitted!");
        };

        // Add a new row to the table
        table.RowCount += 1;
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Add the button in the first column of the new row
        table.Controls.Add(submitButton, 0, table.RowCount - 1);

        // Span across both columns
        table.SetColumnSpan(submitButton, 2);
    }
}