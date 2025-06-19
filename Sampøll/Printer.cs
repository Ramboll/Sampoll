using System.Diagnostics;

namespace Sampøll
{

    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class GridConfig
    {
        public int TopMargin { get; set; } = 0;
        public int BottomMargin { get; set; } = 0;
        public int LeftMargin { get; set; } = 0;
        public int RightMargin { get; set; } = 0;
        public int MiddleMargin { get; set; } = 0;
        public int Columns { get; set; } = 3;
        public int Rows { get; set; } = 7;
    }

    public static class Printer
    {
        public static void Print(
            int filled_pages, 
            int no_number_pages, 
            int empty_pages, 
            GridConfig config, 
            string filePath,
            
            string project_number = "",
            string location = "",
            string sample_type_prefix = "",
            string date = "",
            string initials = ""
            )
        {
            string content = GetHtmlContent(
                filled_pages,
                no_number_pages,
                empty_pages,
                config.Rows,
                config.Columns,
                config.TopMargin,
                config.RightMargin,
                config.BottomMargin,
                config.LeftMargin,
                config.MiddleMargin,
                
                project_number,
                location,
                sample_type_prefix,
                date,
                initials
            );

            string htmlFullPath = Path.GetFullPath(filePath);
            File.WriteAllText(htmlFullPath, content);
        }

        public static string GetHtmlContent(
            int filled_pages,
            int no_number_pages,
            int empty_pages,
            int rows,
            int columns,
            
            double marginTopMm,
            double marginRightMm,
            double marginBottomMm,
            double marginLeftMm,
            double columnSpacingMm,
            
            string project_number = "",
            string location = "",
            string sample_type_prefix = "",
            string date = "",
            string initials = ""
            )
        {
            
            double availableHeightMm = 297 - marginTopMm - marginBottomMm;
            double cellHeightMm = availableHeightMm / rows;

            var sb = new System.Text.StringBuilder();

            sb.AppendLine("<html><head charset='UTF-8'>");
            sb.AppendLine(
                $@"
                <style>
                    @page {{
                        size: A4;
                        margin: 20mm 20mm 20mm 20mm;
                    }}

                    html, body {{
                        margin: 0;
                        padding: 0;
                    }}

                    .page {{
                        width: 100%;
                        height: 297mm;
                        page-break-after: always;
                    }}

                    table {{
                    border-collapse: collapse;
                    width: 100%;
                    height: 100%;
                    table-layout: fixed;
                    }}
                }}

                td {{
                    padding: 0;
                    box-sizing: border-box;
                        height: {{cellHeightMm}}mm;
                    }}
                </style>"
            );
            sb.AppendLine("</head><body>");

            int filledPagesCellIndex = 1;
            for (int p = 0; p < filled_pages; p++)
            {
                sb.AppendLine("<div class='page'><table>");

                for (int r = 0; r < rows; r++)
                {
                    sb.AppendLine("<tr>");
                    for (int c = 0; c < columns; c++)
                    {
                        sb.AppendLine(
                            $"<td>" +
                                $"Sags nr.: { project_number } </br>" +
                                $"Lokation: { location } </br>" +
                                $"Prøve nr.: { sample_type_prefix } {filledPagesCellIndex++} </br>" +
                                $"Dato: { date } </br>" +
                                $"Prøvetager: { initials } </br>" +
                            $"</td>" +
                        $"");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table></div>");
            }
            
            int noNumberPagesCellIndex = 1;
            for (int p = 0; p < no_number_pages; p++)
            {
                sb.AppendLine("<div class='page'><table>");

                for (int r = 0; r < rows; r++)
                {
                    sb.AppendLine("<tr>");
                    for (int c = 0; c < columns; c++)
                    {
                        sb.AppendLine(
                            $"<td>" +
                                $"No Number Page {p + 1}<br/>Cell {noNumberPagesCellIndex++}" +
                            $"</td>" +
                        $"");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table></div>");
            }
            
            int emptyPagesCellIndex = 1;
            for (int p = 0; p < empty_pages; p++)
            {
                sb.AppendLine("<div class='page'><table>");

                for (int r = 0; r < rows; r++)
                {
                    sb.AppendLine("<tr>");
                    for (int c = 0; c < columns; c++)
                    {
                        sb.AppendLine(
                            $"<td>" +
                            $"Empty Page {p + 1}<br/>Cell {noNumberPagesCellIndex++}" +
                            $"</td>" +
                            $"");
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table></div>");
            }

            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        public static void GeneratePdfFromHtml(string htmlFilePath, string pdfFilePath)
        {
            // Find the path to the bundled wkhtmltopdf executable
            string wkhtmltopdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wkhtmltopdf.exe");

            // Ensure the file exists before attempting to start it
            if (!File.Exists(wkhtmltopdfPath))
            {
                throw new FileNotFoundException("wkhtmltopdf.exe not found", wkhtmltopdfPath);
            }

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = wkhtmltopdfPath,
                    Arguments = (
                        "--page-size A4 " +
                        "--encoding utf-8 " +
                        "--zoom 1 " +
                        "--disable-smart-shrinking " +
                        "--margin-top 0 " +
                        "--margin-bottom 0 " +
                        "--margin-left 0 " +
                        "--margin-right 0 " +
                        $"\"{htmlFilePath}\" \"{pdfFilePath}\""
                        ),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"PDF conversion failed. Output: {output}, Error: {error}");
            }

            Process.Start(new ProcessStartInfo(pdfFilePath) { UseShellExecute = true });
        }
    }
}