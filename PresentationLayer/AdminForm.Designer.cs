using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PresentationLayer
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;

        // Tab control and tabs
        private TabControl tabControlReports;
        private TabPage tabPageRevenue;
        private TabPage tabPageOccupancy;
        private TabPage tabPageLoyalCustomers;
        private TabPage tabPageServicePerformance;

        // Revenue tab controls
        private Chart chartRevenue;
        private DataGridView dgvRevenueDetails;
        private Label lblRoomRevenue;
        private Label lblServiceRevenue;
        private Label lblTotalRevenue;
        private Label lblRoomRevenuePercentage;
        private Label lblServiceRevenuePercentage;
        private ComboBox cboTimeGrouping;
        private ComboBox cboYear;

        // Occupancy tab controls
        private Chart chartOccupancy;
        private DataGridView dgvOccupancyDetails;
        private Label lblOverallOccupancy;
        private FlowLayoutPanel flowLayoutRoomTypeOccupancy;
        private ComboBox cboRoomType;

        // Loyal Customers tab controls
        private Chart chartLoyalCustomers;
        private DataGridView dgvLoyalCustomers;
        private Label lblTotalLoyalBookings;
        private Label lblTotalLoyalRevenue;
        private Label lblAvgBookingsPerCustomer;
        private ComboBox cboTopCustomers;
        private TextBox txtCustomerPreferences;

        // Service Performance tab controls
        private Chart chartServiceUsage;
        private Chart chartServiceRevenue;
        private DataGridView dgvServiceDetails;
        private Label lblTotalServicesUsed;
        private Label lblTotalServiceRevenue;
        private Label lblAvgRevenuePerService;
        private Label lblTopService;
        private Label lblBottomService;

        // Common controls
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnRefresh;
        private Button btnExportReports;
        private Label lblStatus;
        private Panel pnlStatusBar;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // === Theme Colors ===
            Color primaryColor = Color.FromArgb(41, 128, 185);    // Blue
            Color secondaryColor = Color.FromArgb(52, 152, 219);  // Light Blue
            Color accentColor = Color.FromArgb(46, 204, 113);     // Green
            Color warningColor = Color.FromArgb(230, 126, 34);    // Orange
            Color backgroundColor = Color.FromArgb(248, 249, 250); // Light Gray
            Color cardBackgroundColor = Color.White;
            Color textColor = Color.FromArgb(44, 62, 80);         // Dark Blue/Gray
            Color borderColor = Color.FromArgb(218, 218, 218);    // Light Gray for borders

            // === Custom Fonts ===
            Font headerFont = new Font("Segoe UI Semibold", 12F, FontStyle.Regular);
            Font subHeaderFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font normalFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            Font smallFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            Font buttonFont = new Font("Segoe UI", 9F, FontStyle.Bold);

            // === Instantiate Controls ===
            this.tabControlReports = new TabControl();
            this.tabPageRevenue = new TabPage("Revenue");
            this.tabPageOccupancy = new TabPage("Occupancy");
            this.tabPageLoyalCustomers = new TabPage("Loyal Customers");
            this.tabPageServicePerformance = new TabPage("Service Performance");

            this.chartRevenue = new Chart();
            this.chartOccupancy = new Chart();
            this.chartLoyalCustomers = new Chart();
            this.chartServiceUsage = new Chart();
            this.chartServiceRevenue = new Chart();

            this.dgvRevenueDetails = new DataGridView();
            this.lblRoomRevenue = new Label();
            this.lblServiceRevenue = new Label();
            this.lblTotalRevenue = new Label();
            this.lblRoomRevenuePercentage = new Label();
            this.lblServiceRevenuePercentage = new Label();
            this.cboTimeGrouping = new ComboBox();
            this.cboYear = new ComboBox();

            this.dgvOccupancyDetails = new DataGridView();
            this.lblOverallOccupancy = new Label();
            this.flowLayoutRoomTypeOccupancy = new FlowLayoutPanel();
            this.cboRoomType = new ComboBox();

            this.dgvLoyalCustomers = new DataGridView();
            this.lblTotalLoyalBookings = new Label();
            this.lblTotalLoyalRevenue = new Label();
            this.lblAvgBookingsPerCustomer = new Label();
            this.cboTopCustomers = new ComboBox();
            this.txtCustomerPreferences = new TextBox();

            this.dgvServiceDetails = new DataGridView();
            this.lblTotalServicesUsed = new Label();
            this.lblTotalServiceRevenue = new Label();
            this.lblAvgRevenuePerService = new Label();
            this.lblTopService = new Label();
            this.lblBottomService = new Label();

            this.dtpStartDate = new DateTimePicker();
            this.dtpEndDate = new DateTimePicker();
            this.btnRefresh = new Button();
            this.btnExportReports = new Button();
            this.lblStatus = new Label();
            this.pnlStatusBar = new Panel();

            // === Helper Function to Style DataGridViews ===
            void StyleDataGridView(DataGridView dgv)
            {
                dgv.BorderStyle = BorderStyle.None;
                dgv.BackgroundColor = cardBackgroundColor;
                dgv.GridColor = Color.FromArgb(230, 230, 230);
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
                dgv.ColumnHeadersDefaultCellStyle.Font = subHeaderFont;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
                dgv.ColumnHeadersHeight = 38;
                dgv.RowHeadersVisible = false;
                dgv.DefaultCellStyle.Font = normalFont;
                dgv.DefaultCellStyle.ForeColor = textColor;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 238, 251);
                dgv.DefaultCellStyle.SelectionForeColor = textColor;
                dgv.RowTemplate.Height = 30;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 252);
                dgv.RowsDefaultCellStyle.Padding = new Padding(5);
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // === Helper Function to Style Charts ===
            void StyleChart(Chart chart)
            {
                chart.BorderlineColor = Color.White;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 1;
                chart.BackColor = cardBackgroundColor;
                chart.Dock = DockStyle.Fill;

                ChartArea area = new ChartArea("ChartArea1");
                area.BackColor = cardBackgroundColor;
                area.AxisX.LineColor = Color.FromArgb(210, 210, 210);
                area.AxisY.LineColor = Color.FromArgb(210, 210, 210);
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
                area.AxisX.LabelStyle.Font = smallFont;
                area.AxisY.LabelStyle.Font = smallFont;
                area.AxisX.LabelStyle.ForeColor = textColor;
                area.AxisY.LabelStyle.ForeColor = textColor;
                chart.ChartAreas.Add(area);

                Legend legend = new Legend("Legend1");
                legend.BackColor = Color.Transparent;
                legend.Font = smallFont;
                legend.ForeColor = textColor;
                legend.Docking = Docking.Top;
                legend.Alignment = StringAlignment.Center;
                chart.Legends.Add(legend);
            }

            // === Helper Function to Create Panels with Shadow Effect ===
            Panel CreatePanelWithShadow(int top, int left, int width, int height)
            {
                Panel shadowPanel = new Panel
                {
                    BackColor = Color.White,
                    Location = new Point(left, top),
                    Size = new Size(width, height),
                    Padding = new Padding(0),
                };

                // Add shadow border effect
                shadowPanel.Paint += (sender, e) =>
                {
                    using (Pen pen = new Pen(borderColor, 1))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, shadowPanel.Width - 1, shadowPanel.Height - 1);
                    }
                };

                return shadowPanel;
            }

            // === Helper Function to Create Stylized Label ===
            Label CreateMetricLabel(string text, Font font, Color foreColor, int paddingTop = 0)
            {
                return new Label
                {
                    Text = text,
                    Font = font,
                    ForeColor = foreColor,
                    AutoSize = true,
                    Margin = new Padding(0, paddingTop, 0, 10),
                };
            }

            // === Stylize Charts ===
            StyleChart(this.chartRevenue);
            StyleChart(this.chartOccupancy);
            StyleChart(this.chartLoyalCustomers);
            StyleChart(this.chartServiceUsage);
            StyleChart(this.chartServiceRevenue);

            // === Stylize DataGridViews ===
            StyleDataGridView(this.dgvRevenueDetails);
            StyleDataGridView(this.dgvOccupancyDetails);
            StyleDataGridView(this.dgvLoyalCustomers);
            StyleDataGridView(this.dgvServiceDetails);

            // === Main TabControl ===
            this.tabControlReports.Dock = DockStyle.Fill;
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.ItemSize = new Size(140, 36);
            this.tabControlReports.Font = normalFont;
            this.tabControlReports.Padding = new Point(15, 3);
            this.tabControlReports.DrawMode = TabDrawMode.OwnerDrawFixed;

            // Custom tab control drawing
            this.tabControlReports.DrawItem += (sender, e) =>
            {
                Graphics g = e.Graphics;
                TabPage tp = this.tabControlReports.TabPages[e.Index];
                Rectangle r = this.tabControlReports.GetTabRect(e.Index);

                bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                if (selected)
                {
                    // Selected tab
                    using (SolidBrush brush = new SolidBrush(cardBackgroundColor))
                    {
                        g.FillRectangle(brush, r);
                    }

                    // Bottom border
                    using (Pen pen = new Pen(primaryColor, 3))
                    {
                        g.DrawLine(pen, r.Left, r.Bottom, r.Right, r.Bottom);
                    }
                }
                else
                {
                    // Unselected tab
                    using (SolidBrush brush = new SolidBrush(backgroundColor))
                    {
                        g.FillRectangle(brush, r);
                    }
                }

                // Draw text
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                Color textClr = selected ? primaryColor : Color.DimGray;
                using (SolidBrush textBrush = new SolidBrush(textClr))
                {
                    g.DrawString(tp.Text, selected ? subHeaderFont : normalFont, textBrush, r, sf);
                }
            };

            // === Revenue Tab Layout ===
            var pnlRevenueFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(15, 10, 15, 10),
                BackColor = cardBackgroundColor,
                BorderStyle = BorderStyle.None
            };

            // Add border to the bottom
            pnlRevenueFilters.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, pnlRevenueFilters.Height - 1, pnlRevenueFilters.Width, pnlRevenueFilters.Height - 1);
                }
            };

            var flpRevenueFilters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true
            };

            // Style comboboxes
            this.cboTimeGrouping.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTimeGrouping.Font = normalFont;
            this.cboTimeGrouping.Width = 120;
            this.cboTimeGrouping.FlatStyle = FlatStyle.Flat;

            this.cboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboYear.Font = normalFont;
            this.cboYear.Width = 120;
            this.cboYear.FlatStyle = FlatStyle.Flat;

            // Add filter controls
            flpRevenueFilters.Controls.Add(CreateMetricLabel("Time Grouping:", normalFont, textColor, 5));
            flpRevenueFilters.Controls.Add(this.cboTimeGrouping);
            flpRevenueFilters.Controls.Add(CreateMetricLabel("Year:", normalFont, textColor, 5));
            flpRevenueFilters.Controls.Add(this.cboYear);
            pnlRevenueFilters.Controls.Add(flpRevenueFilters);

            var tblRevenue = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(15),
                BackColor = backgroundColor
            };
            tblRevenue.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tblRevenue.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblRevenue.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tblRevenue.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // Chart Panel with shadow
            var pnlRevenueChart = CreatePanelWithShadow(0, 0, 100, 100);
            pnlRevenueChart.Dock = DockStyle.Fill;
            pnlRevenueChart.Margin = new Padding(0, 0, 10, 10);
            pnlRevenueChart.Padding = new Padding(10);
            pnlRevenueChart.Controls.Add(this.chartRevenue);
            tblRevenue.Controls.Add(pnlRevenueChart, 0, 0);
            pnlRevenueChart.MinimumSize = new Size(100, 100);

            // Revenue Summary Panel with shadow
            var pnlRevenueMetrics = CreatePanelWithShadow(0, 0, 100, 100);
            pnlRevenueMetrics.Dock = DockStyle.Fill;
            pnlRevenueMetrics.Margin = new Padding(10, 0, 0, 10);
            pnlRevenueMetrics.Padding = new Padding(15);

            var flpRevenueLabels = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true
            };

            // Style the revenue labels
            this.lblTotalRevenue = CreateMetricLabel("Total Revenue: $0", headerFont, primaryColor);
            this.lblRoomRevenue = CreateMetricLabel("Room Revenue: $0", subHeaderFont, textColor, 15);
            this.lblServiceRevenue = CreateMetricLabel("Service Revenue: $0", subHeaderFont, textColor, 5);
            this.lblRoomRevenuePercentage = CreateMetricLabel("Room: 0% of total", normalFont, secondaryColor, 15);
            this.lblServiceRevenuePercentage = CreateMetricLabel("Services: 0% of total", normalFont, warningColor, 5);

            flpRevenueLabels.Controls.Add(this.lblTotalRevenue);
            flpRevenueLabels.Controls.Add(this.lblRoomRevenue);
            flpRevenueLabels.Controls.Add(this.lblServiceRevenue);
            flpRevenueLabels.Controls.Add(this.lblRoomRevenuePercentage);
            flpRevenueLabels.Controls.Add(this.lblServiceRevenuePercentage);

            pnlRevenueMetrics.Controls.Add(flpRevenueLabels);
            tblRevenue.Controls.Add(pnlRevenueMetrics, 1, 0);

            // DataGrid Panel with shadow
            var pnlRevenueDetails = CreatePanelWithShadow(0, 0, 100, 100);
            pnlRevenueDetails.Dock = DockStyle.Fill;
            pnlRevenueDetails.Margin = new Padding(0, 10, 0, 0);
            pnlRevenueDetails.Padding = new Padding(1);
            pnlRevenueDetails.MinimumSize = new Size(100, 100);

            // Add header panel for the data grid
            var pnlDataGridHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblDataGridTitle = new Label
            {
                Text = "Revenue Details",
                Font = subHeaderFont,
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(15, 10)
            };

            pnlDataGridHeader.Controls.Add(lblDataGridTitle);
            pnlRevenueDetails.Controls.Add(pnlDataGridHeader);
            pnlRevenueDetails.Controls.Add(this.dgvRevenueDetails);

            tblRevenue.Controls.Add(pnlRevenueDetails, 0, 1);
            tblRevenue.SetColumnSpan(pnlRevenueDetails, 2);

            this.tabPageRevenue.Controls.Add(tblRevenue);
            this.tabPageRevenue.Controls.Add(pnlRevenueFilters);

            // === Occupancy Tab Layout ===
            var pnlOccupancyFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(15, 10, 15, 10),
                BackColor = cardBackgroundColor,
                BorderStyle = BorderStyle.None
            };

            // Add border to the bottom
            pnlOccupancyFilters.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, pnlOccupancyFilters.Height - 1, pnlOccupancyFilters.Width, pnlOccupancyFilters.Height - 1);
                }
            };

            var flpOccupancyFilters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true
            };

            // Style room type combo
            this.cboRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRoomType.Font = normalFont;
            this.cboRoomType.Width = 120;
            this.cboRoomType.FlatStyle = FlatStyle.Flat;

            flpOccupancyFilters.Controls.Add(CreateMetricLabel("Room Type:", normalFont, textColor, 5));
            flpOccupancyFilters.Controls.Add(this.cboRoomType);
            pnlOccupancyFilters.Controls.Add(flpOccupancyFilters);

            var tblOccupancy = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(15),
                BackColor = backgroundColor
            };
            tblOccupancy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tblOccupancy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblOccupancy.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tblOccupancy.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // Chart Panel with shadow
            var pnlOccupancyChart = CreatePanelWithShadow(0, 0, 100, 100);
            pnlOccupancyChart.Dock = DockStyle.Fill;
            pnlOccupancyChart.Margin = new Padding(0, 0, 10, 10);
            pnlOccupancyChart.Padding = new Padding(10);
            pnlOccupancyChart.Controls.Add(this.chartOccupancy);
            tblOccupancy.Controls.Add(pnlOccupancyChart, 0, 0);
            pnlOccupancyChart.MinimumSize = new Size(100, 100);

            // Occupancy Metrics Panel with shadow
            var pnlOccupancyMetrics = CreatePanelWithShadow(0, 0, 0, 0);
            pnlOccupancyMetrics.Dock = DockStyle.Fill;
            pnlOccupancyMetrics.Margin = new Padding(10, 0, 0, 10);
            pnlOccupancyMetrics.Padding = new Padding(15);

            // Style the occupancy labels
            this.lblOverallOccupancy = CreateMetricLabel("Overall Occupancy: 0%", headerFont, primaryColor);

            // Style the room type occupancy flow panel
            this.flowLayoutRoomTypeOccupancy.Dock = DockStyle.Fill;
            this.flowLayoutRoomTypeOccupancy.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutRoomTypeOccupancy.WrapContents = false;
            this.flowLayoutRoomTypeOccupancy.AutoScroll = true;
            this.flowLayoutRoomTypeOccupancy.Margin = new Padding(0, 15, 0, 0);

            pnlOccupancyMetrics.Controls.Add(this.lblOverallOccupancy);
            pnlOccupancyMetrics.Controls.Add(this.flowLayoutRoomTypeOccupancy);
            tblOccupancy.Controls.Add(pnlOccupancyMetrics, 1, 0);

            // DataGrid Panel with shadow
            var pnlOccupancyDetails = CreatePanelWithShadow(0, 0, 100, 100);
            pnlOccupancyDetails.MinimumSize = new Size(100, 100);
            pnlOccupancyDetails.Dock = DockStyle.Fill;
            pnlOccupancyDetails.Margin = new Padding(0, 10, 0, 0);
            pnlOccupancyDetails.Padding = new Padding(1);

            // Add header panel for the data grid
            var pnlOccupancyGridHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblOccupancyGridTitle = new Label
            {
                Text = "Occupancy Details",
                Font = subHeaderFont,
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(15, 10)
            };

            pnlOccupancyGridHeader.Controls.Add(lblOccupancyGridTitle);
            pnlOccupancyDetails.Controls.Add(pnlOccupancyGridHeader);
            pnlOccupancyDetails.Controls.Add(this.dgvOccupancyDetails);

            tblOccupancy.Controls.Add(pnlOccupancyDetails, 0, 1);
            tblOccupancy.SetColumnSpan(pnlOccupancyDetails, 2);

            this.tabPageOccupancy.Controls.Add(tblOccupancy);
            this.tabPageOccupancy.Controls.Add(pnlOccupancyFilters);

            // === Loyal Customers Tab Layout ===
            var pnlLoyalFilters = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(15, 10, 15, 10),
                BackColor = cardBackgroundColor,
                BorderStyle = BorderStyle.None
            };

            // Add border to the bottom
            pnlLoyalFilters.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, pnlLoyalFilters.Height - 1, pnlLoyalFilters.Width, pnlLoyalFilters.Height - 1);
                }
            };

            var flpLoyalFilters = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true
            };

            // Style top customers combo
            this.cboTopCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTopCustomers.Font = normalFont;
            this.cboTopCustomers.Width = 120;
            this.cboTopCustomers.FlatStyle = FlatStyle.Flat;

            flpLoyalFilters.Controls.Add(CreateMetricLabel("Top Customers:", normalFont, textColor, 5));
            flpLoyalFilters.Controls.Add(this.cboTopCustomers);
            pnlLoyalFilters.Controls.Add(flpLoyalFilters);

            var tblLoyal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(15),
                BackColor = backgroundColor
            };
            tblLoyal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tblLoyal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblLoyal.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tblLoyal.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // Chart Panel with shadow
            var pnlLoyalChart = CreatePanelWithShadow(0, 0, 100, 100);
            pnlLoyalChart.MinimumSize = new Size(100, 100);
            pnlLoyalChart.Dock = DockStyle.Fill;
            pnlLoyalChart.Margin = new Padding(0, 0, 10, 10);
            pnlLoyalChart.Padding = new Padding(10);
            pnlLoyalChart.Controls.Add(this.chartLoyalCustomers);
            tblLoyal.Controls.Add(pnlLoyalChart, 0, 0);

            // Loyal Customers Metrics Panel with shadow
            var pnlLoyalMetrics = CreatePanelWithShadow(0, 0, 0, 0);
            pnlLoyalMetrics.Dock = DockStyle.Fill;
            pnlLoyalMetrics.Margin = new Padding(10, 0, 0, 10);
            pnlLoyalMetrics.Padding = new Padding(15);

            var flpLoyalLabels = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true
            };

            // Style loyal customer metrics
            this.lblTotalLoyalBookings = CreateMetricLabel("Total Loyal Bookings: 0", subHeaderFont, textColor);
            this.lblTotalLoyalRevenue = CreateMetricLabel("Total Loyal Revenue: $0", subHeaderFont, textColor, 5);
            this.lblAvgBookingsPerCustomer = CreateMetricLabel("Avg. Bookings/Customer: 0", subHeaderFont, textColor, 5);

            flpLoyalLabels.Controls.Add(this.lblTotalLoyalBookings);
            flpLoyalLabels.Controls.Add(this.lblTotalLoyalRevenue);
            flpLoyalLabels.Controls.Add(this.lblAvgBookingsPerCustomer);
            pnlLoyalMetrics.Controls.Add(flpLoyalLabels);

            // Customer preferences text box
            this.txtCustomerPreferences.Dock = DockStyle.Bottom;
            this.txtCustomerPreferences.Height = 120;
            this.txtCustomerPreferences.Multiline = true;
            this.txtCustomerPreferences.ReadOnly = true;
            this.txtCustomerPreferences.BackColor = Color.FromArgb(245, 245, 245);
            this.txtCustomerPreferences.BorderStyle = BorderStyle.None;
            this.txtCustomerPreferences.Font = normalFont;
            this.txtCustomerPreferences.ForeColor = textColor;
            this.txtCustomerPreferences.Margin = new Padding(0, 15, 0, 0);
            this.txtCustomerPreferences.Padding = new Padding(5);
            this.txtCustomerPreferences.ScrollBars = ScrollBars.Vertical;
            this.txtCustomerPreferences.Text = "Customer preferences will be displayed here...";

            // Add a nice label above the text box
            var lblPreferences = new Label
            {
                Text = "Preferences & Notes",
                Font = subHeaderFont,
                ForeColor = primaryColor,
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 20, 0, 5)
            };

            pnlLoyalMetrics.Controls.Add(lblPreferences);
            pnlLoyalMetrics.Controls.Add(this.txtCustomerPreferences);

            tblLoyal.Controls.Add(pnlLoyalMetrics, 1, 0);

            // DataGrid Panel with shadow
            var pnlLoyalDetails = CreatePanelWithShadow(0, 0, 100, 100);
            pnlLoyalDetails.MinimumSize = new Size(100, 100);
            pnlLoyalDetails.Dock = DockStyle.Fill;
            pnlLoyalDetails.Margin = new Padding(0, 10, 0, 0);
            pnlLoyalDetails.Padding = new Padding(1);

            // Add header panel for the data grid
            var pnlLoyalGridHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblLoyalGridTitle = new Label
            {
                Text = "Loyal Customer Details",
                Font = subHeaderFont,
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(15, 10)
            };

            pnlLoyalGridHeader.Controls.Add(lblLoyalGridTitle);
            pnlLoyalDetails.Controls.Add(pnlLoyalGridHeader);
            pnlLoyalDetails.Controls.Add(this.dgvLoyalCustomers);

            tblLoyal.Controls.Add(pnlLoyalDetails, 0, 1);
            tblLoyal.SetColumnSpan(pnlLoyalDetails, 2);

            this.tabPageLoyalCustomers.Controls.Add(tblLoyal);
            this.tabPageLoyalCustomers.Controls.Add(pnlLoyalFilters);

            // === Service Performance Tab Layout ===
            var tblService = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(15),
                BackColor = backgroundColor
            };
            tblService.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblService.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblService.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblService.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tblService.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // Chart Panels with shadow
            var pnlServiceUsageChart = CreatePanelWithShadow(0, 0, 100, 100);
            pnlServiceUsageChart.MinimumSize = new Size(100, 100);
            pnlServiceUsageChart.Dock = DockStyle.Fill;
            pnlServiceUsageChart.Margin = new Padding(0, 0, 10, 10);
            pnlServiceUsageChart.Padding = new Padding(10);

            // Add a header for chart
            var lblUsageChartTitle = new Label
            {
                Text = "Service Usage",
                Font = subHeaderFont,
                ForeColor = primaryColor,
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlServiceUsageChart.Controls.Add(lblUsageChartTitle);
            pnlServiceUsageChart.Controls.Add(this.chartServiceUsage);
            tblService.Controls.Add(pnlServiceUsageChart, 0, 0);

            var pnlServiceRevenueChart = CreatePanelWithShadow(0, 0, 100, 100);
            pnlServiceRevenueChart.MinimumSize = new Size(100, 100);
            pnlServiceRevenueChart.Dock = DockStyle.Fill;
            pnlServiceRevenueChart.Margin = new Padding(10, 0, 0, 10);
            pnlServiceRevenueChart.Padding = new Padding(10);

            // Add a header for chart
            var lblRevenueChartTitle = new Label
            {
                Text = "Service Revenue",
                Font = subHeaderFont,
                ForeColor = primaryColor,
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlServiceRevenueChart.Controls.Add(lblRevenueChartTitle);
            pnlServiceRevenueChart.Controls.Add(this.chartServiceRevenue);
            tblService.Controls.Add(pnlServiceRevenueChart, 1, 0);

            // Service Metrics Panel with shadow
            var pnlServiceMetrics = CreatePanelWithShadow(0, 0, 0, 0);
            pnlServiceMetrics.Dock = DockStyle.Fill;
            pnlServiceMetrics.Margin = new Padding(0, 10, 0, 10);
            pnlServiceMetrics.Padding = new Padding(15, 10, 15, 10);

            var flpServiceMetrics = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true
            };

            // Style service metrics
            this.lblTotalServicesUsed = CreateMetricLabel("Total Services: 0", subHeaderFont, textColor);
            this.lblTotalServicesUsed.Margin = new Padding(0, 3, 30, 0);

            this.lblTotalServiceRevenue = CreateMetricLabel("Total Revenue: $0", subHeaderFont, textColor);
            this.lblTotalServiceRevenue.Margin = new Padding(0, 3, 30, 0);

            this.lblAvgRevenuePerService = CreateMetricLabel("Avg. Revenue/Service: $0", subHeaderFont, textColor);
            this.lblAvgRevenuePerService.Margin = new Padding(0, 3, 30, 0);

            this.lblTopService = CreateMetricLabel("Top Service: N/A", normalFont, accentColor);
            this.lblTopService.Margin = new Padding(0, 3, 30, 0);

            this.lblBottomService = CreateMetricLabel("Bottom Service: N/A", normalFont, warningColor);
            this.lblBottomService.Margin = new Padding(0, 3, 0, 0);

            flpServiceMetrics.Controls.Add(this.lblTotalServicesUsed);
            flpServiceMetrics.Controls.Add(this.lblTotalServiceRevenue);
            flpServiceMetrics.Controls.Add(this.lblAvgRevenuePerService);
            flpServiceMetrics.Controls.Add(this.lblTopService);
            flpServiceMetrics.Controls.Add(this.lblBottomService);

            pnlServiceMetrics.Controls.Add(flpServiceMetrics);
            tblService.Controls.Add(pnlServiceMetrics, 0, 1);
            tblService.SetColumnSpan(pnlServiceMetrics, 2);

            // DataGrid Panel with shadow
            var pnlServiceDetails = CreatePanelWithShadow(0, 0, 100, 100);
            pnlServiceDetails.MinimumSize = new Size(100, 100);
            pnlServiceDetails.Dock = DockStyle.Fill;
            pnlServiceDetails.Margin = new Padding(0, 10, 0, 0);
            pnlServiceDetails.Padding = new Padding(1);

            // Add header panel for the data grid
            var pnlServiceGridHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblServiceGridTitle = new Label
            {
                Text = "Service Details",
                Font = subHeaderFont,
                ForeColor = textColor,
                AutoSize = true,
                Location = new Point(15, 10)
            };

            pnlServiceGridHeader.Controls.Add(lblServiceGridTitle);
            pnlServiceDetails.Controls.Add(pnlServiceGridHeader);
            pnlServiceDetails.Controls.Add(this.dgvServiceDetails);

            tblService.Controls.Add(pnlServiceDetails, 0, 2);
            tblService.SetColumnSpan(pnlServiceDetails, 2);

            this.tabPageServicePerformance.Controls.Add(tblService);

            // === Top Panel for Date Range and Actions ===
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = primaryColor
            };

            var flpTopControls = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(15, 12, 15, 0),
                AutoSize = true
            };

            // Style date pickers
            this.dtpStartDate.Width = 120;
            this.dtpStartDate.Font = normalFont;
            this.dtpStartDate.Format = DateTimePickerFormat.Short;

            this.dtpEndDate.Width = 120;
            this.dtpEndDate.Font = normalFont;
            this.dtpEndDate.Format = DateTimePickerFormat.Short;

            // Style buttons
            this.btnRefresh.Width = 100;
            this.btnRefresh.Height = 36;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.BackColor = accentColor;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Font = buttonFont;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Cursor = Cursors.Hand;

            this.btnExportReports.Width = 130;
            this.btnExportReports.Height = 36;
            this.btnExportReports.FlatStyle = FlatStyle.Flat;
            this.btnExportReports.FlatAppearance.BorderSize = 0;
            this.btnExportReports.BackColor = secondaryColor;
            this.btnExportReports.ForeColor = Color.White;
            this.btnExportReports.Font = buttonFont;
            this.btnExportReports.Text = "Export Reports";
            this.btnExportReports.Cursor = Cursors.Hand;

            // Add date labels
            var lblFromDate = new Label
            {
                Text = "From:",
                Font = normalFont,
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(0, 8, 4, 0)
            };

            var lblToDate = new Label
            {
                Text = "To:",
                Font = normalFont,
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(16, 8, 4, 0)
            };

            flpTopControls.Controls.Add(lblFromDate);
            flpTopControls.Controls.Add(this.dtpStartDate);
            flpTopControls.Controls.Add(lblToDate);
            flpTopControls.Controls.Add(this.dtpEndDate);

            // Add a separator
            var lblSeparator = new Label
            {
                Width = 30,
                Height = 1,
                AutoSize = false
            };
            flpTopControls.Controls.Add(lblSeparator);

            flpTopControls.Controls.Add(this.btnRefresh);

            // Add some space
            var lblSpace = new Label
            {
                Width = 10,
                Height = 1,
                AutoSize = false
            };
            flpTopControls.Controls.Add(lblSpace);

            flpTopControls.Controls.Add(this.btnExportReports);

            pnlTop.Controls.Add(flpTopControls);

            // === Status Bar at Bottom ===
            this.pnlStatusBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.None
            };

            // Add top border
            this.pnlStatusBar.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, 0, this.pnlStatusBar.Width, 0);
                }
            };

            this.lblStatus.Dock = DockStyle.Fill;
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblStatus.ForeColor = Color.DimGray;
            this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            this.lblStatus.Text = "Ready";
            this.lblStatus.Padding = new Padding(15, 0, 0, 0);

            this.pnlStatusBar.Controls.Add(this.lblStatus);

            // === Add TabPages to TabControl ===
            this.tabControlReports.TabPages.Add(this.tabPageRevenue);
            this.tabControlReports.TabPages.Add(this.tabPageOccupancy);
            this.tabControlReports.TabPages.Add(this.tabPageLoyalCustomers);
            this.tabControlReports.TabPages.Add(this.tabPageServicePerformance);

            // === Main Form Properties ===
            this.Controls.Add(this.tabControlReports);
            this.Controls.Add(pnlTop);
            this.Controls.Add(this.pnlStatusBar);

            this.Name = "AdminForm";
            this.Text = "Hotel Management System - Admin Dashboard";
            this.ClientSize = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = backgroundColor;
            this.Font = normalFont;
            this.ForeColor = textColor;
            this.MinimumSize = new Size(900, 700);

            // === Event Handlers ===
            this.cboTimeGrouping.SelectedIndexChanged += new EventHandler(this.cboTimeGrouping_SelectedIndexChanged);
            this.cboTopCustomers.SelectedIndexChanged += new EventHandler(this.cboTopCustomers_SelectedIndexChanged);
            this.cboRoomType.SelectedIndexChanged += new EventHandler(this.cboRoomType_SelectedIndexChanged);
            this.cboYear.SelectedIndexChanged += new EventHandler(this.cboYear_SelectedIndexChanged);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.btnExportReports.Click += new EventHandler(this.btnExportReports_Click);
            this.dtpStartDate.ValueChanged += new EventHandler(this.dtpStartDate_ValueChanged);
            this.dtpEndDate.ValueChanged += new EventHandler(this.dtpEndDate_ValueChanged);
            this.Load += new EventHandler(this.AdminForm_Load);

            // Set MinimumSize for TabPages
            this.tabPageRevenue.MinimumSize = new Size(300, 200);
            this.tabPageOccupancy.MinimumSize = new Size(300, 200);
            this.tabPageLoyalCustomers.MinimumSize = new Size(300, 200);
            this.tabPageServicePerformance.MinimumSize = new Size(300, 200);

            // Set MinimumSize for the main form (already present, but ensure it's not removed)
            this.MinimumSize = new Size(900, 700);

            // Optionally, set MinimumSize for TableLayoutPanels if needed
            tblRevenue.MinimumSize = new Size(300, 200);
            tblOccupancy.MinimumSize = new Size(300, 200);
            // ...repeat for tblLoyal, tblService if desired

            // If you have any custom controls or panels, set their MinimumSize as well


            this.ResumeLayout(false);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}