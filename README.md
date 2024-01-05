# [My Shop](https://tdquang7.notion.site/Project-MyShop-aaf1d2578b0d473399fcbd79ea14e0a1)

<aside>
🎯 Tạo ra ứng dụng quản lí cơ bản, hỗ trợ bán hàng

</aside>

# A. Mô tả

Viết chương trình quản lí việc bán hàng cho một cửa hàng nhỏ. Bạn có thể chọn một trong các chủ đề gợi ý sau:

1. Laptop
2. Điện thoại di động
3. Sách
4. Chủ đề nào mà bạn muốn

___ 

## Các chức năng cơ sở (5 điểm)

Cơ sở dữ liệu sử dụng: SQL Server (hoặc CSDL quan hệ tùy chọn tương đương như Postgres/MySQL/MariaDB/MongoDB…) hoặc CSDL mà bạn muốn.

 ### 1. Màn hình đăng nhập (0.25 điểm)

- [x]  Cho nhập username và password để đi vào màn hình chính. Có chức năng lưu username và password ở local để lần sau người dùng không cần đăng nhập lại. Password cần được mã hóa.

- [x]  Cho phép cấu hình thêm thông tin như server, tên database kết nối.

### 2. Màn hình dashboard (0.25 điểm)

  Cung cấp tổng quan về hệ thống đang quản lí, ví dụ:

- [x]  Có tổng cộng bao nhiêu sản phẩm đang bán
- [x]  Có tổng cộng bao nhiêu đơn hàng mới trong tuần / tháng
- [x]  Liệt kê top 5 sản phẩm đang sắp hết hàng (số lượng < 5)

- Ví dụ một dashboard thường thấy sẽ như thế này:
![image](https://github.com/fit-hcmus-k21/my-shop/assets/93416202/bb6683bd-67a1-49e5-b35a-d87049afb05c)

### 3. Quản lí hàng hóa (1.5 điểm)

- [x]  Import dữ liệu gốc ban đầu (loại sản phẩm, danh sách các sản phẩm) từ tập tin Excel hoặc Access.
- [x]  Thao tác với **Loại sản phẩm**: Xem danh sách, Thêm, Xóa, Cập nhật
- [x]  Thao tác với **Sản phẩm**
    - [x]  Xem danh sách theo Loại sản phẩm
        - [x]  Có phân trang
        - [x]  Sắp xếp theo tiêu chí
    - [x]  Xem chi tiết một sản phẩm
        - [x]  Xóa, cập nhật sản phẩm
    - [x]  Thêm mới một sản phẩm
- [x]  Cho phép tìm kiếm sản phẩm theo tên
- [x]  Cho phép lọc lại sản phẩm theo khoảng giá

### 4. Quản lí các đơn hàng (1.5 điểm)

- [x]  Tạo ra các đơn hàng
- [x]  Cho phép xóa một đơn hàng, cập nhật một đơn hàng
- [x]  Cho phép xem danh sách các đơn hàng có phân trang, xem chi tiết một đơn hàng
- [x]  Tìm kiếm các đơn hàng từ ngày đến ngày

### 5. Báo cáo thống kê (1 điểm)

- [x]  Báo cáo doanh thu và lợi nhuận theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)
- [x]  Xem các sản phẩm và số lượng bán theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)

### 6. Cấu hình (0.25 điểm)

- [x]  Cho phép hiệu chỉnh số lượng sản phẩm mỗi trang
- [x]  Cho phép khi chạy chương trình lên thì mở lại màn hình cuối mà lần trước tắt

### 7. Đóng gói thành file cài đặt (0.25 điểm)

- [x]  Cần đóng gói thành file exe để tự cài chương trình vào hệ thống


## Các chức năng gợi ý nâng cao (Tự chọn để được 5 điểm)

- [x]  Sử dụng một thiết kế giao diện tốt lấy từ pinterest (0.5 điểm)
- [ ]  Làm rối mã nguồn (obfuscator) chống dịch ngược (0.25 điểm)
- [ ]  Thêm chế độ dùng thử - cho phép xài full phần mềm trong 15 ngày. Hết 15 ngày bắt đăng kí (mã code hay cách kích hoạt nào đó) (0.5 điểm)
- [x]  Báo cáo các sản phẩm bán chạy trong tuần, trong tháng, trong năm (1 điểm)
- [x]  Bổ sung khuyến mãi giảm giá (1 điểm)
- [x]  Quản lí khách hàng (1 điểm)
- [ ]  Sử dụng giao diện Ribbon (0.25 điểm)
- [ ]  Backup / restore database (0.5 điểm)
- [x]  Tổ chức theo mô hình 3 lớp (1 điểm)
- [ ]  Chương trình có khả năng mở rộng động theo kiến trúc plugin (1 điểm)
- [ ]  Sử dụng mô hình MVVM (1 điểm)
- [ ]  Sử dụng Dependency injection (1 điểm)
- [ ]  Sử dụng DevExpress / Telerik  / Kendo UI  (1 điểm)
- [ ]  Có khả năng cập nhật tính năng mới qua mạng sử dụng ClickOnce(0.5 điểm)
- [ ]  Sử dụng thư viện WinUI mới (1 điểm)
- [ ]  Kết nối API Rest API (1 điểm)
- [ ]  Kết nối GraphQL API (1 điểm)
- [x]  Tự động thay đổi sắp xếp hợp lí các thành phần theo độ rộng màn hình (0.5 điểm)
