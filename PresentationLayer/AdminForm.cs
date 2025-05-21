using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();

            // Initialize date range pickers with default values
            dtpStartDate.Value = DateTime.Now.AddMonths(-3); // Show last 3 months by default
            dtpEndDate.Value = DateTime.Now;

            // Initialize filter combo boxes
            InitializeFilterComboBoxes();
        }

        private void InitializeFilterComboBoxes()
        {
            // Room Type filter
            cboRoomType.Items.Add("All");
            cboRoomType.Items.Add("Single");
            cboRoomType.Items.Add("Double");
            cboRoomType.Items.Add("Family");
            cboRoomType.SelectedIndex = 0;

            // Time grouping filter
            cboTimeGrouping.Items.Add("Day");
            cboTimeGrouping.Items.Add("Week");
            cboTimeGrouping.Items.Add("Month");
            cboTimeGrouping.Items.Add("Quarter");
            cboTimeGrouping.SelectedIndex = 2; // Default to Monthly

            // Year selector
            cboYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 2; i <= currentYear; i++)
            {
                cboYear.Items.Add(i);
            }
            cboYear.SelectedItem = currentYear;

            // Top customers filter
            cboTopCustomers.Items.Add("Top 5");
            cboTopCustomers.Items.Add("Top 10");
            cboTopCustomers.Items.Add("Top 20");
            cboTopCustomers.SelectedIndex = 1; // Default to Top 10
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            _ = LoadAllReportsAsync();
        }

        private async Task LoadAllReportsAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Loading reports...";

            try
            {
                await Task.WhenAll(
                    LoadRevenueReportAsync(),
                    LoadOccupancyRateReportAsync(),
                    LoadLoyalCustomersReportAsync(),
                    LoadServicePerformanceReportAsync()
                );
                lblStatus.Text = "All reports loaded successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reports: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error loading reports.";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        
        #region Revenue Report


        private async Task LoadRevenueReportAsync()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                string timeGrouping = GetTimeGroupingValue();

                DataTable revenueData = await ChartBLL.GetRevenueByDateRange(startDate, endDate, timeGrouping);

                if (revenueData == null || revenueData.Rows.Count == 0)
                {
                    MessageBox.Show("No revenue data found for the selected period.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    chartRevenue.Series.Clear();
                    chartRevenue.Titles.Clear();

                    lblRoomRevenue.Text = "Room Revenue: $0.00";
                    lblServiceRevenue.Text = "Service Revenue: $0.00";
                    lblTotalRevenue.Text = "Total Revenue: $0.00";
                    lblRoomRevenuePercentage.Text = "0.0%";
                    lblServiceRevenuePercentage.Text = "0.0%";

                    dgvRevenueDetails.DataSource = null;
                    return;
                }

                chartRevenue.Series.Clear();
                chartRevenue.ChartAreas[0].AxisX.Title = timeGrouping;
                chartRevenue.ChartAreas[0].AxisY.Title = "Revenue (USD)";
                chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "${0:#,##0}";
                chartRevenue.ChartAreas[0].AxisX.Interval = 1;
                chartRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartRevenue.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);

                Series roomRevenueSeries = new Series("Room Revenue")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.SkyBlue
                };
                Series serviceRevenueSeries = new Series("Service Revenue")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.Orange
                };
                Series totalRevenueSeries = new Series("Total Revenue")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.DarkRed,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8
                };

                foreach (DataRow row in revenueData.Rows)
                {
                    string periodLabel = row["Period"].ToString();

                    decimal roomRevenue = row["RoomRevenue"] != DBNull.Value ? Convert.ToDecimal(row["RoomRevenue"]) : 0;
                    decimal serviceRevenue = row["ServiceRevenue"] != DBNull.Value ? Convert.ToDecimal(row["ServiceRevenue"]) : 0;
                    decimal totalRevenue = row["TotalRevenue"] != DBNull.Value ? Convert.ToDecimal(row["TotalRevenue"]) : 0;

                    roomRevenueSeries.Points.AddXY(periodLabel, roomRevenue);
                    serviceRevenueSeries.Points.AddXY(periodLabel, serviceRevenue);
                    totalRevenueSeries.Points.AddXY(periodLabel, totalRevenue);
                }

                chartRevenue.Series.Add(roomRevenueSeries);
                chartRevenue.Series.Add(serviceRevenueSeries);
                chartRevenue.Series.Add(totalRevenueSeries);

                chartRevenue.Legends[0].Docking = Docking.Top;
                chartRevenue.Legends[0].Alignment = StringAlignment.Center;

                chartRevenue.Titles.Clear();
                chartRevenue.Titles.Add($"Revenue Analysis ({startDate:MM/dd/yyyy} - {endDate:MM/dd/yyyy})");

                decimal totalRoomRevenue = 0;
                decimal totalServiceRevenue = 0;
                foreach (DataRow row in revenueData.Rows)
                {
                    totalRoomRevenue += row["RoomRevenue"] != DBNull.Value ? Convert.ToDecimal(row["RoomRevenue"]) : 0;
                    totalServiceRevenue += row["ServiceRevenue"] != DBNull.Value ? Convert.ToDecimal(row["ServiceRevenue"]) : 0;
                }
                decimal totalOverallRevenue = totalRoomRevenue + totalServiceRevenue;

                lblRoomRevenue.Text = $"Room Revenue: ${totalRoomRevenue:#,##0.00}";
                lblServiceRevenue.Text = $"Service Revenue: ${totalServiceRevenue:#,##0.00}";
                lblTotalRevenue.Text = $"Total Revenue: ${totalOverallRevenue:#,##0.00}";

                double roomRevenuePercentage = 0;
                double serviceRevenuePercentage = 0;
                if (totalOverallRevenue > 0)
                {
                    roomRevenuePercentage = (double)(totalRoomRevenue / totalOverallRevenue * 100);
                    serviceRevenuePercentage = (double)(totalServiceRevenue / totalOverallRevenue * 100);
                }

                lblRoomRevenuePercentage.Text = $"{roomRevenuePercentage:0.0}%";
                lblServiceRevenuePercentage.Text = $"{serviceRevenuePercentage:0.0}%";

                dgvRevenueDetails.DataSource = revenueData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading revenue report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Occupancy Rate Report

        private async Task LoadOccupancyRateReportAsync()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                string timeGrouping = GetTimeGroupingValue();
                string roomTypeFilter = cboRoomType.SelectedItem?.ToString() ?? "All";

                DataTable occupancyData = await ChartBLL.GetOccupancyRateByDateRange(startDate, endDate, timeGrouping);

                if (roomTypeFilter != "All")
                {
                    DataTable filteredData = occupancyData.Clone();
                    var filteredRows = occupancyData.AsEnumerable()
                        .Where(row => row.Field<string>("RoomType") == roomTypeFilter);

                    if (filteredRows.Any())
                        occupancyData = filteredRows.CopyToDataTable();
                    else
                        occupancyData = filteredData;
                }

                chartOccupancy.Series.Clear();
                chartOccupancy.ChartAreas[0].AxisX.Title = timeGrouping;
                chartOccupancy.ChartAreas[0].AxisY.Title = "Occupancy Rate (%)";
                chartOccupancy.ChartAreas[0].AxisY.Maximum = 100;
                chartOccupancy.ChartAreas[0].AxisY.Minimum = 0;
                chartOccupancy.ChartAreas[0].AxisX.Interval = 1;
                chartOccupancy.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartOccupancy.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);

                var roomTypes = occupancyData.AsEnumerable()
                    .Select(row => row.Field<string>("RoomType"))
                    .Distinct();

                Series overallSeries = new Series("Overall")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.Black,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8
                };

                foreach (var roomType in roomTypes)
                {
                    Series series = new Series(roomType)
                    {
                        ChartType = SeriesChartType.Line,
                        BorderWidth = 2
                    };

                    switch (roomType)
                    {
                        case "Single": series.Color = Color.Blue; break;
                        case "Double": series.Color = Color.Green; break;
                        case "Family": series.Color = Color.Red; break;
                        default: series.Color = Color.Purple; break;
                    }

                    var roomData = occupancyData.AsEnumerable()
                        .Where(row => row.Field<string>("RoomType") == roomType);

                    foreach (var row in roomData)
                    {
                        string periodLabel = row.Field<string>("Period");
                        series.Points.AddXY(periodLabel, row.Field<double>("OccupancyRate"));
                    }

                    chartOccupancy.Series.Add(series);
                }

                var distinctPeriods = occupancyData.AsEnumerable()
                    .Select(row => row.Field<string>("Period"))
                    .Distinct()
                    .ToList();

                var periodOccupancyRates = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<double>>();
                foreach (DataRow row in occupancyData.Rows)
                {
                    string periodLabel = row.Field<string>("Period");
                    if (!periodOccupancyRates.ContainsKey(periodLabel))
                        periodOccupancyRates[periodLabel] = new System.Collections.Generic.List<double>();
                    periodOccupancyRates[periodLabel].Add(row.Field<double>("OccupancyRate"));
                }

                foreach (string periodLabel in distinctPeriods)
                {
                    if (periodOccupancyRates.ContainsKey(periodLabel) && periodOccupancyRates[periodLabel].Any())
                    {
                        double avgOccupancy = periodOccupancyRates[periodLabel].Average();
                        overallSeries.Points.AddXY(periodLabel, avgOccupancy);
                    }
                }

                chartOccupancy.Series.Add(overallSeries);

                chartOccupancy.Legends[0].Docking = Docking.Top;
                chartOccupancy.Legends[0].Alignment = StringAlignment.Center;

                chartOccupancy.Titles.Clear();
                chartOccupancy.Titles.Add($"Room Occupancy Rate ({startDate:MM/dd/yyyy} - {endDate:MM/dd/yyyy})");

                double overallAvgOccupancy = 0;
                if (occupancyData.Rows.Count > 0)
                {
                    overallAvgOccupancy = occupancyData.AsEnumerable()
                        .Average(row => row.Field<double>("OccupancyRate"));
                }

                var roomTypeStats = from row in occupancyData.AsEnumerable()
                                    group row by row.Field<string>("RoomType") into g
                                    select new
                                    {
                                        RoomType = g.Key,
                                        AvgOccupancy = g.Average(r => r.Field<double>("OccupancyRate"))
                                    };

                lblOverallOccupancy.Text = $"Overall Occupancy: {overallAvgOccupancy:0.0}%";

                flowLayoutRoomTypeOccupancy.Controls.Clear();
                foreach (var stat in roomTypeStats)
                {
                    Label lbl = new Label();
                    lbl.Text = $"{stat.RoomType}: {stat.AvgOccupancy:0.0}%";
                    lbl.AutoSize = true;
                    lbl.Margin = new Padding(3);
                    flowLayoutRoomTypeOccupancy.Controls.Add(lbl);
                }

                dgvOccupancyDetails.DataSource = occupancyData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading occupancy rate report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Loyal Customers Report

        private async Task LoadLoyalCustomersReportAsync()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                int topCount = 10;
                if (cboTopCustomers.SelectedItem != null)
                {
                    var text = cboTopCustomers.SelectedItem.ToString();
                    if (text.StartsWith("Top "))
                    {
                        int.TryParse(text.Substring(4), out topCount);
                    }
                }

                DataTable loyalCustomersData = await ChartBLL.GetLoyalCustomers(startDate, endDate, topCount);

                chartLoyalCustomers.Series.Clear();
                chartLoyalCustomers.ChartAreas[0].AxisX.Title = "Guest";
                chartLoyalCustomers.ChartAreas[0].AxisY.Title = "Number of Bookings";
                chartLoyalCustomers.ChartAreas[0].AxisY2.Title = "Total Spent (USD)";
                chartLoyalCustomers.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                chartLoyalCustomers.ChartAreas[0].AxisY2.LabelStyle.Format = "${0:#,##0}";
                chartLoyalCustomers.ChartAreas[0].AxisX.Interval = 1;
                chartLoyalCustomers.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartLoyalCustomers.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);

                Series bookingsSeries = new Series("Number of Bookings")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.SteelBlue
                };
                Series spentSeries = new Series("Total Spent")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = Color.DarkGreen,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8,
                    YAxisType = AxisType.Secondary
                };

                foreach (DataRow row in loyalCustomersData.Rows)
                {
                    string guestName = row.Field<string>("CustomerName");
                    int bookingCount = row.Field<int>("BookingCount");
                    decimal totalSpent = row.Field<decimal>("TotalSpent");

                    bookingsSeries.Points.AddXY(guestName, bookingCount);
                    spentSeries.Points.AddXY(guestName, totalSpent);
                }

                chartLoyalCustomers.Series.Add(bookingsSeries);
                chartLoyalCustomers.Series.Add(spentSeries);

                chartLoyalCustomers.Legends[0].Docking = Docking.Top;
                chartLoyalCustomers.Legends[0].Alignment = StringAlignment.Center;

                chartLoyalCustomers.Titles.Clear();
                chartLoyalCustomers.Titles.Add($"Top {topCount} Loyal Customers ({startDate:MM/dd/yyyy} - {endDate:MM/dd/yyyy})");

                int totalLoyalBookings = loyalCustomersData.AsEnumerable().Sum(r => r.Field<int>("BookingCount"));
                decimal totalLoyalRevenue = loyalCustomersData.AsEnumerable().Sum(r => r.Field<decimal>("TotalSpent"));
                double avgBookingsPerCustomer = loyalCustomersData.AsEnumerable().Any()
                    ? loyalCustomersData.AsEnumerable().Average(r => r.Field<int>("BookingCount"))
                    : 0;

                lblTotalLoyalBookings.Text = $"Total Bookings: {totalLoyalBookings}";
                lblTotalLoyalRevenue.Text = $"Total Revenue: ${totalLoyalRevenue:#,##0.00}";
                lblAvgBookingsPerCustomer.Text = $"Avg Bookings: {avgBookingsPerCustomer:0.0}";

                txtCustomerPreferences.Text = "Preferred Room Types: (not available)";

                dgvLoyalCustomers.DataSource = loyalCustomersData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading loyal customers report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Service Performance Report

        private async Task LoadServicePerformanceReportAsync()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                DataTable serviceData = await ChartBLL.GetServicePerformance(startDate, endDate);

                chartServiceUsage.Series.Clear();
                chartServiceRevenue.Series.Clear();

                Series usageSeries = new Series("Usage Count")
                {
                    ChartType = SeriesChartType.Pie,
                    Label = "#PERCENT{P0}",
                    LegendText = "#VALX"
                };
                usageSeries["PieLabelStyle"] = "Outside";
                usageSeries["PieLineColor"] = "Black";

                Series revenueSeries = new Series("Revenue")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.DarkGreen,
                    IsValueShownAsLabel = true,
                    LabelFormat = "${0:#,##0}"
                };

                chartServiceRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "${0:#,##0}";
                chartServiceRevenue.ChartAreas[0].AxisX.Interval = 1;
                chartServiceRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartServiceRevenue.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);

                DataView dv = serviceData.DefaultView;
                dv.Sort = "TotalRevenue DESC";
                DataTable sortedServiceData = dv.ToTable();

                foreach (DataRow row in sortedServiceData.Rows)
                {
                    string serviceName = row.Field<string>("ServiceName");
                    int usageCount = row.Field<int>("UsageCount");
                    decimal revenue = row.Field<decimal>("TotalRevenue");

                    usageSeries.Points.AddXY(serviceName, usageCount);
                    revenueSeries.Points.AddXY(serviceName, revenue);
                }

                chartServiceUsage.Series.Add(usageSeries);
                chartServiceRevenue.Series.Add(revenueSeries);

                chartServiceUsage.Legends[0].Docking = Docking.Bottom;
                chartServiceUsage.Legends[0].Alignment = StringAlignment.Center;
                chartServiceRevenue.Legends[0].Enabled = false;

                chartServiceUsage.Titles.Clear();
                chartServiceUsage.Titles.Add("Service Usage Distribution");

                chartServiceRevenue.Titles.Clear();
                chartServiceRevenue.Titles.Add("Service Revenue Contribution");

                int totalServicesUsed = serviceData.AsEnumerable().Sum(r => r.Field<int>("UsageCount"));
                decimal totalServiceRevenue = serviceData.AsEnumerable().Sum(r => r.Field<decimal>("TotalRevenue"));
                decimal avgRevenuePerService = serviceData.AsEnumerable().Any()
                    ? serviceData.AsEnumerable().Average(r => r.Field<decimal>("TotalRevenue"))
                    : 0;

                lblTotalServicesUsed.Text = $"Total Services Used: {totalServicesUsed}";
                lblTotalServiceRevenue.Text = $"Total Service Revenue: ${totalServiceRevenue:#,##0.00}";
                lblAvgRevenuePerService.Text = $"Avg Revenue Per Service: ${avgRevenuePerService:#,##0.00}";

                if (sortedServiceData.Rows.Count > 0)
                {
                    var topService = sortedServiceData.Rows[0];
                    var bottomService = sortedServiceData.Rows[sortedServiceData.Rows.Count - 1];

                    lblTopService.Text = $"Top Service: {topService["ServiceName"]} (${(decimal)topService["TotalRevenue"]:#,##0.00})";
                    lblBottomService.Text = $"Bottom Service: {bottomService["ServiceName"]} (${(decimal)bottomService["TotalRevenue"]:#,##0.00})";
                }
                else
                {
                    lblTopService.Text = "Top Service: No data available";
                    lblBottomService.Text = "Bottom Service: No data available";
                }

                dgvServiceDetails.DataSource = sortedServiceData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading service performance report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllReportsAsync();
        }

        private async void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Value = dtpEndDate.Value.AddMonths(-1);
                return;
            }

            await LoadAllReportsAsync();
        }

        private async void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value < dtpStartDate.Value)
            {
                MessageBox.Show("End date cannot be before start date.", "Invalid Date Range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Value = dtpStartDate.Value.AddMonths(1);
                return;
            }

            await LoadAllReportsAsync();
        }

        private async void cboTimeGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Task.WhenAll(
                LoadRevenueReportAsync(),
                LoadOccupancyRateReportAsync()
            );
        }

        private async void cboTopCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadLoyalCustomersReportAsync();
        }

        private void btnExportReports_Click(object sender, EventArgs e)
        {
            try
            {
                string exportPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\HotelManageApp\\Reports";
                if (!System.IO.Directory.Exists(exportPath))
                {
                    System.IO.Directory.CreateDirectory(exportPath);
                }

                string dateRange = $"{dtpStartDate.Value:yyyyMMdd}_to_{dtpEndDate.Value:yyyyMMdd}";

                chartRevenue.SaveImage($"{exportPath}\\Revenue_{dateRange}.png", ChartImageFormat.Png);
                chartOccupancy.SaveImage($"{exportPath}\\Occupancy_{dateRange}.png", ChartImageFormat.Png);
                chartLoyalCustomers.SaveImage($"{exportPath}\\LoyalCustomers_{dateRange}.png", ChartImageFormat.Png);
                chartServiceUsage.SaveImage($"{exportPath}\\ServiceUsage_{dateRange}.png", ChartImageFormat.Png);
                chartServiceRevenue.SaveImage($"{exportPath}\\ServiceRevenue_{dateRange}.png", ChartImageFormat.Png);

                if (dgvRevenueDetails.DataSource != null)
                    ExportDataTableToCsv((DataTable)dgvRevenueDetails.DataSource, $"{exportPath}\\Revenue_{dateRange}.csv");

                if (dgvOccupancyDetails.DataSource != null)
                    ExportDataTableToCsv((DataTable)dgvOccupancyDetails.DataSource, $"{exportPath}\\Occupancy_{dateRange}.csv");

                if (dgvLoyalCustomers.DataSource != null)
                    ExportDataTableToCsv((DataTable)dgvLoyalCustomers.DataSource, $"{exportPath}\\LoyalCustomers_{dateRange}.csv");

                if (dgvServiceDetails.DataSource != null)
                    ExportDataTableToCsv((DataTable)dgvServiceDetails.DataSource, $"{exportPath}\\ServiceDetails_{dateRange}.csv");

                MessageBox.Show($"All reports have been exported to:\n{exportPath}", "Export Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting reports: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTimeGroupingValue()
        {
            switch (cboTimeGrouping.SelectedItem?.ToString())
            {
                case "Day": return "day";
                case "Week": return "week";
                case "Month": return "month";
                case "Quarter": return "quarter";
                default: return "month";
            }
        }

        private void ExportDataTableToCsv(DataTable dt, string filePath)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                System.IO.File.WriteAllText(filePath, "No data available", Encoding.UTF8);
                return;
            }

            StringBuilder sb = new StringBuilder();

            var headers = dt.Columns.Cast<DataColumn>().Select(col => col.ColumnName);
            sb.AppendLine(string.Join(",", headers));

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field =>
                {
                    string value = field?.ToString() ?? string.Empty;
                    if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
                    {
                        value = "\"" + value.Replace("\"", "\"\"") + "\"";
                    }
                    return value;
                });
                sb.AppendLine(string.Join(",", fields));
            }

            System.IO.File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private async void cboRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadOccupancyRateReportAsync();
        }

        private async void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadRevenueReportAsync();
        }


        #endregion

    }
}
