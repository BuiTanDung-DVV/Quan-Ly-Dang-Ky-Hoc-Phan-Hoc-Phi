CREATE DATABASE QLDKHP;
USE QLDKHP;

-- 1. BẢNG KHOA
CREATE TABLE Departments (
    DeptID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Office NVARCHAR(100)
);

-- 2. BẢNG GIẢNG VIÊN
CREATE TABLE Lecturers (
    LecturerID INT IDENTITY(1,1) PRIMARY KEY,
    LecturerCode VARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 3. BẢNG NGÀNH HỌC
CREATE TABLE Majors (
    MajorID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 4. BẢNG SINH VIÊN
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentCode VARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(10),
    DateOfBirth DATE,
    Email VARCHAR(100),
    Phone VARCHAR(15),
    Address NVARCHAR(255),
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID),
    AdmissionYear INT,
    Status NVARCHAR(50) DEFAULT N'Đang học'
);

-- 5. BẢNG KỲ HỌC
CREATE TABLE AcademicTerms (
    TermID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsCurrent BIT DEFAULT 0
);

-- 6. BẢNG MÔN HỌC
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL,
    TuitionPerCredit DECIMAL(10,2) NOT NULL,
    DeptID INT FOREIGN KEY REFERENCES Departments(DeptID)
);

-- 7. BẢNG LỚP HỌC PHẦN
CREATE TABLE ClassSections (
    SectionID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
    TermID INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    LecturerID INT FOREIGN KEY REFERENCES Lecturers(LecturerID),
    Schedule NVARCHAR(100),
    Room NVARCHAR(50),
    MaxStudents INT DEFAULT 60
);

-- 8. BẢNG ĐĂNG KÝ HỌC
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
    SectionID INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    RegisterDate DATE DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT N'Đang học'
);

-- 9. BẢNG HÓA ĐƠN HỌC PHÍ
CREATE TABLE Invoices (
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
    TermID INT FOREIGN KEY REFERENCES AcademicTerms(TermID),
    TotalAmount DECIMAL(12,2) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsPaid BIT DEFAULT 0
);

-- 10. BẢNG CHI TIẾT HÓA ĐƠN
CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    SectionID INT FOREIGN KEY REFERENCES ClassSections(SectionID),
    Amount DECIMAL(12,2) NOT NULL
);

-- 11. BẢNG THANH TOÁN
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceID INT FOREIGN KEY REFERENCES Invoices(InvoiceID),
    PaymentDate DATETIME DEFAULT GETDATE(),
    AmountPaid DECIMAL(12,2) NOT NULL,
    Method NVARCHAR(50),
    Note NVARCHAR(255)
);

-- 12. BẢNG NGƯỜI DÙNG HỆ THỐNG
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role NVARCHAR(50),
    LinkedStudentID INT NULL FOREIGN KEY REFERENCES Students(StudentID),
    LinkedLecturerID INT NULL FOREIGN KEY REFERENCES Lecturers(LecturerID)
);

INSERT INTO Departments (Code, Name, Office)
VALUES 
('CNTT', N'Công nghệ thông tin', N'Tòa A1-101'),
('QTKD', N'Quản trị kinh doanh', N'Tòa B2-202'),
('NNA', N'Ngôn ngữ Anh', N'Tòa C3-303');

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


-- STORED PROCEDURES TÌM KIẾM SINH VIÊN
CREATE OR ALTER PROC TKTTSinhVien @StudentCode VARCHAR(20)
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE StudentCode = @StudentCode
END;

EXECUTE TKTTSinhVien 'SV001'

CREATE OR ALTER PROC TKTTSinhVien1 @FullName NVARCHAR(100)
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE FullName = @FullName
END;

EXECUTE TKTTSinhVien1 N'Nguyễn Thị Mai'

CREATE OR ALTER PROC TKTTSinhVien2 @DeptID INT
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE DeptID = @DeptID
END;

EXECUTE TKTTSinhVien2 1

CREATE OR ALTER PROC TKTTSinhVien3 @AdmissionYear INT
AS
BEGIN
    SELECT StudentID, StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status
    FROM Students WHERE AdmissionYear = @AdmissionYear
END;

EXECUTE TKTTSinhVien3 2021


-- STORED PROCEDURES TÌM KIẾM MÔN HỌC

CREATE OR ALTER PROC TKTTMonHoc @CourseCode VARCHAR(20)
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE Code = @CourseCode
END;

EXECUTE TKTTMonHoc 'CT101'

CREATE OR ALTER PROC TKTTMonHoc1 @CourseName NVARCHAR(100)
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE Name = @CourseName
END;

EXECUTE TKTTMonHoc1 N'Lập trình cơ bản'

CREATE OR ALTER PROC TKTTMonHoc2 @DeptID INT
AS
BEGIN
    SELECT CourseID, Code, Name, Credits, TuitionPerCredit, DeptID
    FROM Courses WHERE DeptID = @DeptID
END;

EXECUTE TKTTMonHoc2 1


-- STORED PROCEDURES TÌM KIẾM HÓA ĐƠN
CREATE OR ALTER PROC TKTTHoaDon @InvoiceID INT
AS
BEGIN
    SELECT InvoiceID, StudentID, TermID, TotalAmount, CreatedDate, IsPaid
    FROM Invoices WHERE InvoiceID = @InvoiceID
END;

EXECUTE TKTTHoaDon 1

CREATE OR ALTER PROC TKTTHoaDon1 @StudentID INT
AS
BEGIN
    SELECT InvoiceID, StudentID, TermID, TotalAmount, CreatedDate, IsPaid
    FROM Invoices WHERE StudentID = @StudentID
END;

EXECUTE TKTTHoaDon1 1

CREATE OR ALTER PROC TKTTHoaDon2 @TermID INT
AS
BEGIN
    SELECT InvoiceID, StudentID, TermID, TotalAmount, CreatedDate, IsPaid
    FROM Invoices WHERE TermID = @TermID
END;

EXECUTE TKTTHoaDon2 2

CREATE OR ALTER PROC TKTTHoaDon3 @IsPaid BIT
AS
BEGIN
    SELECT InvoiceID, StudentID, TermID, TotalAmount, CreatedDate, IsPaid
    FROM Invoices WHERE IsPaid = @IsPaid
END;

EXECUTE TKTTHoaDon3 0


-- STORED PROCEDURES THÊM, SỬA, XÓA SINH VIÊN
CREATE PROCEDURE ThemSinhVien
    @StudentCode VARCHAR(20),
    @FullName NVARCHAR(100),
    @Gender NVARCHAR(10),
    @DateOfBirth DATE,
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address NVARCHAR(255),
    @DeptID INT,
    @AdmissionYear INT,
    @Status NVARCHAR(50) = N'Đang học'
AS
BEGIN
    INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status)
    VALUES (@StudentCode, @FullName, @Gender, @DateOfBirth, @Email, @Phone, @Address, @DeptID, @AdmissionYear, @Status)
END

CREATE PROCEDURE SuaSinhVien
    @StudentID INT,
    @StudentCode VARCHAR(20),
    @FullName NVARCHAR(100),
    @Gender NVARCHAR(10),
    @DateOfBirth DATE,
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address NVARCHAR(255),
    @DeptID INT,
    @AdmissionYear INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Students 
    SET StudentCode = @StudentCode, FullName = @FullName, Gender = @Gender,
        DateOfBirth = @DateOfBirth, Email = @Email, Phone = @Phone,
        Address = @Address, DeptID = @DeptID, AdmissionYear = @AdmissionYear, Status = @Status
    WHERE StudentID = @StudentID
END

CREATE PROCEDURE XoaSinhVien
    @StudentID INT
AS
BEGIN
    DELETE FROM Students WHERE StudentID = @StudentID
END


-- STORED PROCEDURES THÊM, SỬA, XÓA MÔN HỌC
CREATE PROCEDURE ThemMonHoc
    @Code VARCHAR(20),
    @Name NVARCHAR(100),
    @Credits INT,
    @TuitionPerCredit DECIMAL(10,2),
    @DeptID INT
AS
BEGIN
    INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID)
    VALUES (@Code, @Name, @Credits, @TuitionPerCredit, @DeptID)
END

CREATE PROCEDURE SuaMonHoc
    @CourseID INT,
    @Code VARCHAR(20),
    @Name NVARCHAR(100),
    @Credits INT,
    @TuitionPerCredit DECIMAL(10,2),
    @DeptID INT
AS
BEGIN
    UPDATE Courses 
    SET Code = @Code, Name = @Name, Credits = @Credits,
        TuitionPerCredit = @TuitionPerCredit, DeptID = @DeptID
    WHERE CourseID = @CourseID
END

CREATE PROCEDURE XoaMonHoc
    @CourseID INT
AS
BEGIN
    DELETE FROM Courses WHERE CourseID = @CourseID
END


-- STORED PROCEDURES THÊM, SỬA, XÓA HÓA ĐƠN
CREATE PROCEDURE ThemHoaDon
    @StudentID INT,
    @TermID INT,
    @TotalAmount DECIMAL(12,2),
    @IsPaid BIT = 0
AS
BEGIN
    INSERT INTO Invoices (StudentID, TermID, TotalAmount, IsPaid)
    VALUES (@StudentID, @TermID, @TotalAmount, @IsPaid)
END

CREATE PROCEDURE SuaHoaDon
    @InvoiceID INT,
    @StudentID INT,
    @TermID INT,
    @TotalAmount DECIMAL(12,2),
    @IsPaid BIT
AS
BEGIN
    UPDATE Invoices 
    SET StudentID = @StudentID, TermID = @TermID, 
        TotalAmount = @TotalAmount, IsPaid = @IsPaid
    WHERE InvoiceID = @InvoiceID
END

CREATE PROCEDURE XoaHoaDon
    @InvoiceID INT
AS
BEGIN
    DELETE FROM Invoices WHERE InvoiceID = @InvoiceID
END


-- XEM DỮ LIỆU
SELECT * FROM Departments;
SELECT * FROM Lecturers;
SELECT * FROM Students;
SELECT * FROM Courses;
SELECT * FROM ClassSections;
SELECT * FROM Enrollments;
SELECT * FROM Invoices;
SELECT * FROM Payments;
SELECT * FROM Users;


-- TRIGGER TỰ ĐỘNG TẠO/CẬP NHẬT HÓA ĐƠN KHI ĐĂNG KÝ
CREATE OR ALTER TRIGGER tr_CreateOrUpdateInvoice
ON Enrollments
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Xử lý khi thêm đăng ký mới
    IF EXISTS(SELECT * FROM inserted)
    BEGIN
        DECLARE @StudentID INT, @TermID INT, @SectionID INT;
        
        DECLARE enrollment_cursor CURSOR FOR
        SELECT i.StudentID, cs.TermID, i.SectionID
        FROM inserted i
        JOIN ClassSections cs ON i.SectionID = cs.SectionID;
        
        OPEN enrollment_cursor;
        FETCH NEXT FROM enrollment_cursor INTO @StudentID, @TermID, @SectionID;
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            DECLARE @InvoiceID INT;
            DECLARE @Amount DECIMAL(12,2);
            
            -- Tính tiền môn học
            SELECT @Amount = c.Credits * c.TuitionPerCredit
            FROM ClassSections cs
            JOIN Courses c ON cs.CourseID = c.CourseID
            WHERE cs.SectionID = @SectionID;
            
            -- Kiểm tra xem đã có hóa đơn cho học kỳ này chưa
            SELECT @InvoiceID = InvoiceID 
            FROM Invoices 
            WHERE StudentID = @StudentID AND TermID = @TermID;
            
            IF @InvoiceID IS NULL
            BEGIN
                -- Tạo hóa đơn mới
                INSERT INTO Invoices (StudentID, TermID, TotalAmount, IsPaid)
                VALUES (@StudentID, @TermID, @Amount, 0);
                
                SET @InvoiceID = SCOPE_IDENTITY();
            END
            ELSE
            BEGIN
                -- Cập nhật tổng tiền
                UPDATE Invoices 
                SET TotalAmount = TotalAmount + @Amount 
                WHERE InvoiceID = @InvoiceID;
            END
            
            -- Thêm chi tiết hóa đơn
            INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount)
            VALUES (@InvoiceID, @SectionID, @Amount);
            
            FETCH NEXT FROM enrollment_cursor INTO @StudentID, @TermID, @SectionID;
        END
        
        CLOSE enrollment_cursor;
        DEALLOCATE enrollment_cursor;
    END
    
    -- Xử lý khi hủy đăng ký
    IF EXISTS(SELECT * FROM deleted)
    BEGIN
        DECLARE @DelStudentID INT, @DelTermID INT, @DelSectionID INT;
        
        DECLARE delete_cursor CURSOR FOR
        SELECT d.StudentID, cs.TermID, d.SectionID
        FROM deleted d
        JOIN ClassSections cs ON d.SectionID = cs.SectionID;
        
        OPEN delete_cursor;
        FETCH NEXT FROM delete_cursor INTO @DelStudentID, @DelTermID, @DelSectionID;
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            DECLARE @DelInvoiceID INT;
            DECLARE @DelAmount DECIMAL(12,2);
            
            -- Lấy thông tin hóa đơn
            SELECT @DelInvoiceID = InvoiceID 
            FROM Invoices 
            WHERE StudentID = @DelStudentID AND TermID = @DelTermID;
            
            -- Lấy số tiền cần trừ
            SELECT @DelAmount = Amount 
            FROM InvoiceDetails 
            WHERE InvoiceID = @DelInvoiceID AND SectionID = @DelSectionID;
            
            -- Xóa chi tiết hóa đơn
            DELETE FROM InvoiceDetails 
            WHERE InvoiceID = @DelInvoiceID AND SectionID = @DelSectionID;
            
            -- Cập nhật tổng tiền
            UPDATE Invoices 
            SET TotalAmount = TotalAmount - @DelAmount 
            WHERE InvoiceID = @DelInvoiceID;
            
            -- Xóa hóa đơn nếu không còn môn nào
            IF NOT EXISTS(SELECT 1 FROM InvoiceDetails WHERE InvoiceID = @DelInvoiceID)
            BEGIN
                DELETE FROM Invoices WHERE InvoiceID = @DelInvoiceID;
            END
            
            FETCH NEXT FROM delete_cursor INTO @DelStudentID, @DelTermID, @DelSectionID;
        END
        
        CLOSE delete_cursor;
        DEALLOCATE delete_cursor;
    END
END;

--TRIGGER KIỂM TRA SỈ SỐ LỚP
CREATE OR ALTER TRIGGER tr_CheckClassCapacity
ON Enrollments
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @StudentID INT, @SectionID INT;
    DECLARE @MaxStudents INT, @CurrentStudents INT;
    
    DECLARE capacity_cursor CURSOR FOR
    SELECT StudentID, SectionID FROM inserted;
    
    OPEN capacity_cursor;
    FETCH NEXT FROM capacity_cursor INTO @StudentID, @SectionID;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Lấy sĩ số tối đa
        SELECT @MaxStudents = MaxStudents 
        FROM ClassSections 
        WHERE SectionID = @SectionID;
        
        -- Đếm số sinh viên hiện tại
        SELECT @CurrentStudents = COUNT(*) 
        FROM Enrollments 
        WHERE SectionID = @SectionID 
            AND Status IN (N'Đang học', N'Đã duyệt');
        
        -- Kiểm tra đã đăng ký chưa
        IF EXISTS(SELECT 1 FROM Enrollments 
                  WHERE StudentID = @StudentID AND SectionID = @SectionID)
        BEGIN
            RAISERROR(N'Sinh viên đã đăng ký lớp học phần này!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra sĩ số
        IF @CurrentStudents >= @MaxStudents
        BEGIN
            RAISERROR(N'Lớp học phần đã đầy!', 16, 1);
            RETURN;
        END
        
        -- Thêm đăng ký hợp lệ
        INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
        SELECT StudentID, SectionID, RegisterDate, Status
        FROM inserted
        WHERE StudentID = @StudentID AND SectionID = @SectionID;
        
        FETCH NEXT FROM capacity_cursor INTO @StudentID, @SectionID;
    END
    
    CLOSE capacity_cursor;
    DEALLOCATE capacity_cursor;
END;

--FUNCTION TÍNH TỔNG HỌC PHÍ CỦA SINH VIÊN TRONG HỌC KỲ
CREATE OR ALTER FUNCTION fn_GetStudentTuitionByTerm(@StudentID INT, @TermID INT)
RETURNS DECIMAL(12,2)
AS
BEGIN
    DECLARE @TotalTuition DECIMAL(12,2) = 0;
    
    SELECT @TotalTuition = SUM(c.Credits * c.TuitionPerCredit)
    FROM Enrollments e
    JOIN ClassSections cs ON e.SectionID = cs.SectionID
    JOIN Courses c ON cs.CourseID = c.CourseID
    WHERE e.StudentID = @StudentID 
        AND cs.TermID = @TermID
        AND e.Status IN (N'Đang học', N'Đã duyệt');
    
    RETURN ISNULL(@TotalTuition, 0);
END;

--STORED PROCEDURE TẠO HÓA ĐƠN CHO HỌC KỲ
CREATE OR ALTER PROCEDURE sp_CreateInvoiceForTerm
    @StudentID INT,
    @TermID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @InvoiceID INT;
    DECLARE @TotalAmount DECIMAL(12,2);
    
    -- Kiểm tra đã có hóa đơn chưa
    IF EXISTS(SELECT 1 FROM Invoices WHERE StudentID = @StudentID AND TermID = @TermID)
    BEGIN
        PRINT N'Đã có hóa đơn cho học kỳ này';
        RETURN;
    END
    
    -- Tính tổng học phí
    SET @TotalAmount = dbo.fn_GetStudentTuitionByTerm(@StudentID, @TermID);
    
    IF @TotalAmount = 0
    BEGIN
        PRINT N'Sinh viên chưa đăng ký môn học nào';
        RETURN;
    END
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Tạo hóa đơn
        INSERT INTO Invoices (StudentID, TermID, TotalAmount, IsPaid)
        VALUES (@StudentID, @TermID, @TotalAmount, 0);
        
        SET @InvoiceID = SCOPE_IDENTITY();
        
        -- Tạo chi tiết hóa đơn
        INSERT INTO InvoiceDetails (InvoiceID, SectionID, Amount)
        SELECT 
            @InvoiceID,
            cs.SectionID,
            c.Credits * c.TuitionPerCredit
        FROM Enrollments e
        JOIN ClassSections cs ON e.SectionID = cs.SectionID
        JOIN Courses c ON cs.CourseID = c.CourseID
        WHERE e.StudentID = @StudentID 
            AND cs.TermID = @TermID
            AND e.Status IN (N'Đang học', N'Đã duyệt');
        
        COMMIT TRANSACTION;
        
        PRINT N'Tạo hóa đơn thành công';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;