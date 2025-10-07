CREATE DATABASE QLDKHP;
USE QLDKHP;

-- 1. BẢNG KHOA
CREATE TABLE Departments (
    DeptID      INT IDENTITY(1,1) PRIMARY KEY,
    Code        VARCHAR(20) UNIQUE NOT NULL,
    Name        NVARCHAR(100) NOT NULL,
    Office      NVARCHAR(100)
);

-- 2. BẢNG GIẢNG VIÊN
CREATE TABLE Lecturers (
    LecturerID      INT IDENTITY(1,1) PRIMARY KEY,
    LecturerCode    VARCHAR(20) UNIQUE NOT NULL,
    FullName        NVARCHAR(100) NOT NULL,
    Email           VARCHAR(100),
    DeptID          INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 3. BẢNG NGÀNH HỌC
CREATE TABLE Majors (
    MajorID     INT IDENTITY(1,1) PRIMARY KEY,
    Code        VARCHAR(20) UNIQUE NOT NULL,
    Name        NVARCHAR(100) NOT NULL,
    DeptID      INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 4. BẢNG SINH VIÊN
CREATE TABLE Students (
    StudentID       INT IDENTITY(1,1) PRIMARY KEY,
    StudentCode     VARCHAR(20) UNIQUE NOT NULL,
    FullName        NVARCHAR(100) NOT NULL,
    Gender          NVARCHAR(10),
    DateOfBirth     DATE,
    Email           VARCHAR(100),
    Phone           VARCHAR(15),
    Address         NVARCHAR(255),
    DeptID          INT FOREIGN KEY REFERENCES Departments(DeptID),
    AdmissionYear   INT,
    Status          NVARCHAR(50) DEFAULT N'Đang học'
);

-- 5. BẢNG KỲ HỌC
CREATE TABLE AcademicTerms (
    TermID          INT IDENTITY(1,1) PRIMARY KEY,
    Code            VARCHAR(20) UNIQUE NOT NULL,
    Name            NVARCHAR(100) NOT NULL,
    StartDate       DATE NOT NULL,
    EndDate         DATE NOT NULL,
    IsCurrent       BIT DEFAULT 0
);

-- 6. BẢNG MÔN HỌC
CREATE TABLE Courses (
    CourseID        INT IDENTITY(1,1) PRIMARY KEY,
    Code            VARCHAR(20) UNIQUE NOT NULL,
    Name            NVARCHAR(100) NOT NULL,
    Credits         INT NOT NULL,
    TuitionPerCredit DECIMAL(10,2) NOT NULL,
    DeptID          INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 7. BẢNG LỚP HỌC PHẦN
CREATE TABLE ClassSections (
    SectionID       INT IDENTITY(1,1) PRIMARY KEY,
    CourseID        INT FOREIGN KEY REFERENCES Courses(CourseID),
    TermID          INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    LecturerID      INT FOREIGN KEY REFERENCES Lecturers(LecturerID),
    Schedule        NVARCHAR(100),
    Room            NVARCHAR(50),
    MaxStudents     INT DEFAULT 60
);

-- 8. BẢNG ĐĂNG KÝ HỌC
CREATE TABLE Enrollments (
    EnrollmentID    INT IDENTITY(1,1) PRIMARY KEY,
    StudentID       INT FOREIGN KEY REFERENCES Students(StudentID),
    SectionID       INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    RegisterDate    DATE DEFAULT GETDATE(),
    Status          NVARCHAR(50) DEFAULT N'Đang học'
);

-- 9. BẢNG HÓA ĐƠN HỌC PHÍ
CREATE TABLE Invoices (
    InvoiceID       INT IDENTITY(1,1) PRIMARY KEY,
    StudentID       INT FOREIGN KEY REFERENCES Students(StudentID),
    TermID          INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    TotalAmount     DECIMAL(12,2) NOT NULL,
    CreatedDate     DATETIME DEFAULT GETDATE(),
    IsPaid          BIT DEFAULT 0
);

-- 10. BẢNG CHI TIẾT HÓA ĐƠN
CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID       INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    SectionID       INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    Amount          DECIMAL(12,2) NOT NULL
);

-- 11. BẢNG THANH TOÁN
CREATE TABLE Payments (
    PaymentID       INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID       INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    PaymentDate     DATETIME DEFAULT GETDATE(),
    AmountPaid      DECIMAL(12,2) NOT NULL,
    Method          NVARCHAR(50),
    Note            NVARCHAR(255)
);

-- 12. BẢNG NGƯỜI DÙNG HỆ THỐNG
CREATE TABLE Users (
    UserID          INT IDENTITY(1,1) PRIMARY KEY,
    Username        VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash    VARCHAR(255) NOT NULL,
    Role            NVARCHAR(50),
    LinkedStudentID INT NULL FOREIGN KEY REFERENCES Students(StudentID),
    LinkedLecturerID INT NULL FOREIGN KEY REFERENCES Lecturers(LecturerID)
);


INSERT INTO Departments (Code, Name, Office)
VALUES 
('CNTT', N'Công nghệ thông tin', N'Tòa A1-101'),
('QTKD', N'Quản trị kinh doanh', N'Tòa B2-202'),
('NNA',  N'Ngôn ngữ Anh', N'Tòa C3-303');

INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID)
VALUES
('GV001', N'Nguyễn Văn Hòa', 'hoa.nguyen@univ.edu.vn', 1),
('GV002', N'Trần Thị Hạnh', 'hanh.tran@univ.edu.vn', 2),
('GV003', N'Lê Minh Tâm', 'tam.le@univ.edu.vn', 3);

INSERT INTO Majors (Code, Name, DeptID)
VALUES
('CNPM', N'Công nghệ phần mềm', 1),
('QTKD', N'Quản trị kinh doanh tổng hợp', 2),
('TAUD', N'Tiếng Anh ứng dụng', 3);

INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear)
VALUES
('SV001', N'Nguyễn Thị Mai', N'Nữ', '2003-04-12', 'mai.nguyen@stu.edu.vn', '0901234567', N'Hà Nội', 1, 2021),
('SV002', N'Phạm Minh Đức', N'Nam', '2002-11-30', 'duc.pham@stu.edu.vn', '0907654321', N'Hải Phòng', 2, 2020),
('SV003', N'Lê Ngọc Anh', N'Nữ', '2003-08-20', 'anh.le@stu.edu.vn', '0989123456', N'Đà Nẵng', 3, 2021);

INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent)
VALUES
('HK231', N'Học kỳ 1 - Năm học 2023-2024', '2023-09-01', '2024-01-15', 0),
('HK232', N'Học kỳ 2 - Năm học 2023-2024', '2024-02-15', '2024-06-30', 1);

INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID)
VALUES
('CT101', N'Lập trình cơ bản', 3, 450000, 1),
('CT201', N'Cơ sở dữ liệu', 3, 450000, 1),
('QT101', N'Nguyên lý quản trị', 3, 420000, 2),
('EN101', N'Tiếng Anh giao tiếp 1', 2, 400000, 3);

INSERT INTO ClassSections (CourseID, TermID, LecturerID, Schedule, Room, MaxStudents)
VALUES
(1, 2, 1, N'Thứ 2 - Tiết 1,2,3', N'A1-201', 60),
(2, 2, 1, N'Thứ 4 - Tiết 1,2,3', N'A1-202', 60),
(3, 2, 2, N'Thứ 3 - Tiết 2,3,4', N'B2-101', 60),
(4, 2, 3, N'Thứ 5 - Tiết 1,2', N'C3-201', 50);

INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
VALUES
(1, 1, GETDATE(), N'Đang học'),
(1, 2, GETDATE(), N'Đang học'),
(2, 3, GETDATE(), N'Đang học'),
(3, 4, GETDATE(), N'Đang học');

INSERT INTO Invoices (StudentID, TermID, TotalAmount, CreatedDate, IsPaid)
VALUES
(1, 2, 2700000, GETDATE(), 0),
(2, 2, 1260000, GETDATE(), 1),
(3, 2, 800000, GETDATE(), 0);

INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount)
VALUES
(1, 1, 1350000),
(1, 2, 1350000),
(2, 3, 1260000),
(3, 4, 800000);

INSERT INTO Payments (InvoiceID, PaymentDate, AmountPaid, Method, Note)
VALUES
(2, GETDATE(), 1260000, N'Chuyển khoản', N'Đã thanh toán đủ kỳ này');

INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID)
VALUES
('sv_mai', '123456', N'Sinh viên', 1, NULL),
('sv_duc', '123456', N'Sinh viên', 2, NULL),
('gv_hoa', '123456', N'Giảng viên', NULL, 1),
('admin', 'admin123', N'Quản trị', NULL, NULL);

SELECT * FROM Departments;
SELECT * FROM Lecturers;
SELECT * FROM Students;        
SELECT * FROM Courses;
SELECT * FROM ClassSections;
SELECT * FROM Enrollments;
SELECT * FROM Invoices;
SELECT * FROM Payments;
SELECT * FROM Users;
