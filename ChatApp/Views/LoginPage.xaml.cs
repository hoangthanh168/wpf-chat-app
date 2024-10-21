using System.Windows;
using System.Windows.Controls;

namespace ChatApp.Views
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các ô nhập liệu
            string name = NameTextBox.Text;
            string ipAddress = IpTextBox.Text;

            // Kiểm tra nếu thông tin hợp lệ
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ipAddress))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên và địa chỉ IP.");
                return;
            }

            // Kiểm tra địa chỉ IP có hợp lệ không
            if (!System.Net.IPAddress.TryParse(ipAddress, out _))
            {
                MessageBox.Show("Địa chỉ IP không hợp lệ.");
                return;
            }

            // Nếu thông tin hợp lệ, tiến hành đăng nhập
            MessageBox.Show($"Đăng nhập thành công!\nTên: {name}\nIP: {ipAddress}");

            // Tại đây bạn có thể tiếp tục điều hướng hoặc xử lý logic đăng nhập
        }
    }
}
