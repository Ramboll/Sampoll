namespace Sampøll
{
    public partial class LabelOptionsForm : Form
    {
        private TableLayoutPanel _table;
        // Fields for validation
        private TextBox _projectNumberInput, _locationInput, _initialsInput;
        private NumericUpDown _filledPagesInput, _emptyNumbersPagesInput, _emptyPagesInput;
        private ComboBox _pageTypeComboBox, _sampleTypeComboBox, _drillNumberComboBox;
        private TextBox _depthInput;
        private DateTimePicker _singleDatePicker, _dateRangeFromPicker, _dateRangeToPicker;
        private Label _dateRangeFromLabel, _dateRangeToLabel;
        private CheckBox _useDateRangeCheckbox;

        public LabelOptionsForm()
        {
            InitializeComponent();
            
            AddHeader();
            AddTableLayout();
            AddProjectNumberField();
            AddLocationField();
            AddInitialsFields();
            AddDateField();
            AddSeparator();
            
            AddSampleTypeFields();
            AddSeparator();
            
            AddFilledPagesField();
            AddEmptyNumbersPagesField();
            AddEmptyPagesField();
            AddPageTypeOptionsField();
            AddSubmitButton();

            this.Text = "SAMPØLL";
            int viewWidth = (Constants.Margin * 2) * 2 + Constants.LabelWidth + Constants.InputWidth;
            int viewHeight = (Constants.Margin * 2) + (Constants.RowHeight * _table.RowCount) + Constants.HeaderHeight;
            this.Size = new System.Drawing.Size(viewWidth, viewHeight);
            this.Icon = new Icon("icon.ico");
        }

        private void AddHeader()
        {
            var header = new Label
            {
                Text = "SAMPØLL",
                Location = new Point(Constants.Margin, Constants.Margin),
                Font = new Font("MonoSpace", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(Constants.HeaderWidth, Constants.HeaderHeight),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DodgerBlue
            };
            this.Controls.Add(header);
        }

        private void AddTableLayout()
        {
            _table = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 0, // will grow as you add rows
                Location = new Point(Constants.Margin, Constants.Margin + Constants.HeaderHeight),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Constants.LabelWidth));
            _table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Controls.Add(_table);
        }

        private void AddRow(Control label, Control input)
        {
            _table.RowCount += 1;
            _table.RowStyles.Add(new RowStyle(SizeType.Absolute, Constants.RowHeight));
            _table.Controls.Add(label, 0, _table.RowCount - 1);
            _table.Controls.Add(input, 1, _table.RowCount - 1);
        }

        private void AddProjectNumberField()
        {
            var prjnumLabel = new Label { Text = "Project Number:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _projectNumberInput = new TextBox { Size = new Size(Constants.InputWidth, Constants.InputHeight) };
            AddRow(prjnumLabel, _projectNumberInput);
        }

        private void AddLocationField()
        {
            var locationLabel = new Label { Text = "Location:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _locationInput = new TextBox { Size = new Size(Constants.InputWidth, Constants.InputHeight) };
            AddRow(locationLabel, _locationInput);
        }

        private void AddDateField()
        {
            var dateLabel = new Label { Text = "Date:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true };
            _singleDatePicker = new DateTimePicker();
            _dateRangeFromLabel = new Label { Text = "From:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Visible = false };
            _dateRangeFromPicker = new DateTimePicker { Visible = false };
            _dateRangeToLabel = new Label { Text = "To:", TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Visible = false };
            _dateRangeToPicker = new DateTimePicker { Visible = false };
            _useDateRangeCheckbox = new CheckBox { Text = "Date Range" };
            _useDateRangeCheckbox.CheckedChanged += UseDateRangeCheckbox_CheckedChanged;

            var dateContainer = new FlowLayoutPanel { AutoSize = true };
            dateContainer.Controls.Add(_singleDatePicker);
            dateContainer.Controls.Add(_dateRangeFromLabel);
            dateContainer.Controls.Add(_dateRangeFromPicker);
            dateContainer.Controls.Add(_dateRangeToLabel);
            dateContainer.Controls.Add(_dateRangeToPicker);

            _table.RowCount += 1;
            _table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _table.Controls.Add(dateLabel, 0, _table.RowCount - 1);
            _table.Controls.Add(dateContainer, 1, _table.RowCount - 1);

            _table.RowCount += 1;
            _table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _table.Controls.Add(_useDateRangeCheckbox, 0, _table.RowCount - 1);
            _table.SetColumnSpan(_useDateRangeCheckbox, 2);
        }

        private void UseDateRangeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bool useRange = _useDateRangeCheckbox.Checked;
            _singleDatePicker.Visible = !useRange;
            _dateRangeFromLabel.Visible = useRange;
            _dateRangeFromPicker.Visible = useRange;
            _dateRangeToLabel.Visible = useRange;
            _dateRangeToPicker.Visible = useRange;
            int heightChange = 50 + Constants.Margin;
            this.Size = new Size(this.Width, this.Height + (useRange ? heightChange : -heightChange));
        }

        private void AddInitialsFields()
        {
            var initialsLabel = new Label { Text = "Initials:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _initialsInput = new TextBox { Size = new Size(Constants.InputWidth, Constants.InputHeight) };
            AddRow(initialsLabel, _initialsInput);
        }

        private void AddSeparator()
        {
            _table.RowCount += 1;
            _table.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));

            var separator = new Label
            {
                Text = "-------------------------------------------------",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };

            _table.Controls.Add(separator, 0, _table.RowCount - 1);
            _table.SetColumnSpan(separator, 2);
        }

        private void AddFilledPagesField()
        {
            var filledPagesLabel = new Label { Text = "Pages:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _filledPagesInput = new NumericUpDown { Size = new Size(Constants.InputWidth, Constants.InputHeight), Minimum = 0 };
            _filledPagesInput.Value = 1;
            AddRow(filledPagesLabel, _filledPagesInput);
        }

        private void AddEmptyNumbersPagesField()
        {
            var emptyNumbersPagesLabel = new Label { Text = "Empty Numbers Pages:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _emptyNumbersPagesInput = new NumericUpDown { Size = new Size(Constants.InputWidth, Constants.InputHeight), Minimum = 0 };
            AddRow(emptyNumbersPagesLabel, _emptyNumbersPagesInput);
        }

        private void AddEmptyPagesField()
        {
            var emptyPagesLabel = new Label { Text = "Empty Pages:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _emptyPagesInput = new NumericUpDown { Size = new Size(Constants.InputWidth, Constants.InputHeight), Minimum = 0 };
            AddRow(emptyPagesLabel, _emptyPagesInput);
        }

        private void AddSubmitButton()
        {
            var submitButton = new Button
            {
                Text = "Submit",
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };
            submitButton.Click += SubmitButton_Click;

            _table.RowCount += 1;
            _table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _table.Controls.Add(submitButton, 0, _table.RowCount - 1);
            _table.SetColumnSpan(submitButton, 2);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                string selectedPaperTypeId = _pageTypeComboBox.SelectedItem.ToString();
                string samplePrefix = _sampleTypeComboBox.SelectedItem.ToString() switch
                {
                    "Material sample" => "M",
                    "Drilling Sample" => $"D{_drillNumberComboBox.Text}",
                    "Surface Sample (Not done)" => "S",
                    _ => throw new InvalidOperationException("Unknown sample type")
                };

                PaperDefinition config = PaperDefinitions.GetDefinitionById(selectedPaperTypeId);

                string htmlFilePath = Path.Combine(Application.StartupPath, "grid.html");
                string pdfFilePath = Path.Combine(Application.StartupPath, $"Sampøll_labels-{FormatDate}.pdf");

                // Generate HTML file based on config
                Printer.Print(
                    Decimal.ToInt32(_filledPagesInput.Value),
                    Decimal.ToInt32(_emptyNumbersPagesInput.Value),
                    Decimal.ToInt32(_emptyPagesInput.Value),
                    
                    new GridConfig
                    {
                        TopMargin = config.MarginTop,
                        BottomMargin = config.MarginBottom,
                        LeftMargin = config.MarginLeft,
                        RightMargin = config.MarginRight,
                        MiddleMargin = config.MarginMiddle,
                        Columns = config.Columns,
                        Rows = config.Rows
                    },
                    htmlFilePath,
                    
                    _projectNumberInput.Text,
                    _locationInput.Text,
                    samplePrefix,
                    FormatDate,
                    _initialsInput.Text
                );

                // Generate PDF from the HTML file and open it in the browser for preview
                Printer.GeneratePdfFromHtml(htmlFilePath, pdfFilePath);
            }
        }

        private bool ValidateFields()
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(_projectNumberInput.Text))
            {
                errorMessages.Add("Project Number is required.");
            }

            if (string.IsNullOrWhiteSpace(_locationInput.Text))
            {
                errorMessages.Add("Location is required.");
            }

            if (string.IsNullOrWhiteSpace(_initialsInput.Text))
            {
                errorMessages.Add("Initials are required.");
            }

            if (_useDateRangeCheckbox.Checked)
            {
                if (_dateRangeFromPicker.Value.Date == _dateRangeToPicker.Value.Date)
                {
                    errorMessages.Add("Date range 'From' and 'To' cannot be the same.");
                }
            }

            if (_sampleTypeComboBox.SelectedItem?.ToString() == "Drilling Sample")
            {
                if (string.IsNullOrWhiteSpace(_drillNumberComboBox.Text))
                {
                    errorMessages.Add("Drill Number is required.");
                }

                if (string.IsNullOrWhiteSpace(_depthInput.Text))
                {
                    errorMessages.Add("Max Depth is required.");
                }
            }
            
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        
        private string FormatDate
        {
            get
            {
                if (_useDateRangeCheckbox.Checked)
                {
                    return $"{_dateRangeFromPicker.Value:yyyy-MM-dd} to {_dateRangeToPicker.Value:yyyy-MM-dd}";
                }
                else
                {
                    return _singleDatePicker.Value.ToString("yyyy-MM-dd");
                }
            }
        }

        private void AddPageTypeOptionsField()
        {
            var pageTypeLabel = new Label { Text = "Page Type:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _pageTypeComboBox = new ComboBox { Size = new Size(Constants.InputWidth, Constants.InputHeight) };
            _pageTypeComboBox.Items.AddRange(new[] { "2x7", "3x7", "3x8" });
            _pageTypeComboBox.SelectedIndex = 0;
            AddRow(pageTypeLabel, _pageTypeComboBox);
        }

        private void AddSampleTypeFields()
        {
            var sampleTypeLabel = new Label { Text = "Sample Type:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight) };
            _sampleTypeComboBox = new ComboBox { Size = new Size(Constants.InputWidth, Constants.InputHeight) };
            _sampleTypeComboBox.Items.AddRange(new[] { "Material sample", "Drilling Sample", "Surface Sample (Not done)" });
            _sampleTypeComboBox.SelectedIndex = 0;

            var drillNumberLabel = new Label { Text = "Drill Number:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight), Visible = false };
            _drillNumberComboBox = new ComboBox { Size = new Size(Constants.InputWidth, Constants.InputHeight), Visible = false };
            _drillNumberComboBox.Items.AddRange(new[] { "1", "10", "20", "30", "40" });
            _drillNumberComboBox.SelectedIndex = 0;

            var depthLabel = new Label { Text = "Max Depth:", Size = new Size(Constants.LabelWidth, Constants.LabelHeight), Visible = false };
            _depthInput = new TextBox { Size = new Size(Constants.InputWidth, Constants.InputHeight), Visible = false };

            AddRow(sampleTypeLabel, _sampleTypeComboBox);
            AddRow(drillNumberLabel, _drillNumberComboBox);
            AddRow(depthLabel, _depthInput);

            _sampleTypeComboBox.SelectedIndexChanged += (sender, e) =>
            {
                bool isDrilling = _sampleTypeComboBox.SelectedItem?.ToString() == "Drilling Sample";
                drillNumberLabel.Visible = isDrilling;
                _drillNumberComboBox.Visible = isDrilling;
                depthLabel.Visible = isDrilling;
                _depthInput.Visible = isDrilling;
            };
            
        }
    }
}