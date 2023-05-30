﻿GO
CREATE DATABASE QLHSHVT
GO

USE QLHSHVT;

CREATE TABLE THAMSO
(
	MaThamSo VARCHAR(4) PRIMARY KEY,
	GiaTriToiThieu INT NOT NULL,
	GiaTriToiDa INT NOT NULL
)

INSERT INTO THAMSO VALUES('HPHI',300000,650000)
INSERT INTO THAMSO VALUES('TUOI',14,18)

CREATE TABLE GIAOVIEN
(
	MaGV VARCHAR(4) PRIMARY KEY,
	HoGV NVARCHAR(50) NOT NULL,
	TenGV NVARCHAR(50) NOT NULL,
	GioiTinh BIT NOT NULL,
)

INSERT INTO GIAOVIEN VALUES('GV01',N'Nguyễn Huy','Hoàn',1)
INSERT INTO GIAOVIEN VALUES('GV02', N'Nguyễn Thị', N'Thảo', 0)
INSERT INTO GIAOVIEN VALUES('GV03', N'Nguyễn Đình', N'Thắng', 1)
INSERT INTO GIAOVIEN VALUES('GV04', N'Lê Hồng', N'Sơn', 1)
INSERT INTO GIAOVIEN VALUES('GV05', N'Phạm Thanh', N'Hương', 0)
INSERT INTO GIAOVIEN VALUES('GV06', N'Trần Văn', N'Tâm', 1)
INSERT INTO GIAOVIEN VALUES('GV07', N'Ngô Thị', N'Hồng', 0)
INSERT INTO GIAOVIEN VALUES('GV08', N'Lê Minh', N'Thành', 1)
INSERT INTO GIAOVIEN VALUES('GV09', N'Đặng Hoàng', N'Sơn', 1)
INSERT INTO GIAOVIEN VALUES('GV10', N'Võ Thị', N'Anh', 0)

CREATE TABLE NGANH
(
	MaNganh VARCHAR(4) PRIMARY KEY,
	TenNganh NVARCHAR(15) NOT NULL
)

CREATE TABLE KHOI
(
	MaKhoi VARCHAR(1) PRIMARY KEY,
	TenKhoi NVARCHAR(10) NOT NULL
)

INSERT INTO KHOI VALUES('A','Khối A')
INSERT INTO KHOI VALUES('B','Khối B')
INSERT INTO KHOI VALUES('C','Khối C')

CREATE TABLE LOP
(
	MaLop VARCHAR(2) PRIMARY KEY,
	MaNganh VARCHAR(4) NOT NULL,
	MaGVCN VARCHAR(4) REFERENCES GIAOVIEN(MaGV)
)

INSERT INTO LOP(MaLop,MaNganh) VALUES('A1','A') 
INSERT INTO LOP(MaLop,MaNganh) VALUES('A2','A')
INSERT INTO LOP(MaLop,MaNganh) VALUES('B1','B') 
INSERT INTO LOP(MaLop,MaNganh) VALUES('B2','B') 
INSERT INTO LOP(MaLop,MaNganh) VALUES('C1','C')
INSERT INTO LOP(MaLop,MaNganh) VALUES('C2','C') 

CREATE TABLE PHUHUYNH
(
	MaPH VARCHAR(6) PRIMARY KEY,
	HoPH NVARCHAR(50) NOT NULL,
	TenPH NVARCHAR(25) NOT NULL,
	SDT VARCHAR(10) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	NgheNghiep NVARCHAR(25) NOT NULL
)

INSERT INTO PHUHUYNH VALUES('PH0001','Nguyen Thi','Quy','0123456789','ntq1955@gmail.com','Noi tro')
INSERT INTO PHUHUYNH VALUES('PH0002','Ton That Thi','Minh','047285933','tttm1923@gmail.com','Buon Ban')
INSERT INTO PHUHUYNH VALUES('PH0003','Tran Phi','Hung','0862750376','tph1932@gmail.com','Xay Dung')
INSERT INTO PHUHUYNH VALUES('PH0004','Nguyen Lan','Li','0237583942','nll1944@gmail.com','Noi tro')
INSERT INTO PHUHUYNH VALUES('PH0005','Le Van ','Thao','0267259243','lvt1933@gmail.com','Buon Ban')
INSERT INTO PHUHUYNH VALUES('PH0006','Nguyen Ngoc','Lam','012569686','nnl1953@gmail.com','Lai Xe')
INSERT INTO PHUHUYNH VALUES('PH0007','Lam Minh','Ti','0862753435','lmt1966@gmail.com','Cong Nhan')
INSERT INTO PHUHUYNH VALUES('PH0008','Ton Thu','Minh','0728185184','ttm1927@gmail.com','Xay Dung')
INSERT INTO PHUHUYNH VALUES('PH0009','Nguyen Le','Khanh','0223571773','nlk1951@gmail.com','Noi tro')
INSERT INTO PHUHUYNH VALUES('PH0010','Tran Thi','Mai','0987654321','ttm1990@gmail.com','Giao Vien')
INSERT INTO PHUHUYNH VALUES('PH0011','Pham Thi','Huong','0912345678','pth1995@gmail.com','Y Ta')
INSERT INTO PHUHUYNH VALUES('PH0012','Le Thi','My','0909090909','ltm1988@gmail.com','Kinh Doanh')
INSERT INTO PHUHUYNH VALUES('PH0013','Tran Thi','Hoa','0911111111','tth1980@gmail.com','Bac Si')
INSERT INTO PHUHUYNH VALUES('PH0014','Nguyen Thi','Trang','0888888888','ntt1992@gmail.com','Ky Su')
INSERT INTO PHUHUYNH VALUES('PH0015','Hoang Thi','Phuong','0977777777','htp1998@gmail.com','Sinh Vien')
INSERT INTO PHUHUYNH VALUES('PH0016','Nguyen Van','Ti','0976543210','nvn1991@gmail.com','Giao Vien')
INSERT INTO PHUHUYNH VALUES('PH0017','Le Thi','Anh','0987654321','lta1996@gmail.com','Y Ta')
INSERT INTO PHUHUYNH VALUES('PH0018','Tran Thi','Duyen','0999999999','ttm1989@gmail.com','Kinh Doanh')
INSERT INTO PHUHUYNH VALUES('PH0019','Hoang Van','Cuong','0900000000','hvh1981@gmail.com','Bac Si')
INSERT INTO PHUHUYNH VALUES('PH0020','Nguyen Thi','Chau','0912345678','ntc1990@gmail.com','Giáo viên')

CREATE TABLE TONGIAO 
(
	MaTonGiao VARCHAR(4) PRIMARY KEY,
	TenTonGiao NVARCHAR(100) NOT NULL
)

INSERT INTO TONGIAO (MaTonGiao, TenTonGiao)
VALUES 
	('TG01', N'Phật giáo'),
	('TG02', N'Tin Lành'),
	('TG03', N'Công giáo'),
	('TG04', N'Hòa Hảo'),
	('TG05', N'Hồi giáo'),
	('TG06', N'Đạo Bửu Sơn Kỳ Hương'),
	('TG07', N'Đạo Tứ Ân Hiếu Nghĩa'),
	('TG08', N'Đạo Minh Đạo'),
	('TG09', N'Đạo Đức Giáo');

CREATE TABLE QUOCTICH
(
	MaQuocTich VARCHAR(5) PRIMARY KEY,
	TenQuocTich NVARCHAR(100) NOT NULL
)

INSERT INTO QUOCTICH (MaQuocTich, TenQuocTich)
VALUES 
	('QT001', N'Việt Nam'),
	('QT002', N'Thái Lan'),
	('QT003', N'Philippines'),
	('QT004', N'Malaysia'),
	('QT005', N'Indonesia'),
	('QT006', N'Singapore'),
	('QT007', N'Myanmar'),
	('QT008', N'Lào'),
	('QT009', N'Campuchia'),
	('QT010', N'Đông Timor');

CREATE TABLE HOCSINH
(
	MaHS VARCHAR(8) PRIMARY KEY,
	HoHS NVARCHAR(50) NOT NULL,
	TenHS NVARCHAR(50) NOT NULL,
	AnhHS NVARCHAR(100) NOT NULL,
	GioiTinh BIT NOT NULL,
	MaLop VARCHAR(2) REFERENCES LOP(MaLop) NOT NULL,
	MaPH VARCHAR(6) REFERENCES PHUHUYNH(MaPH) NOT NULL,
	NgaySinh DATE NOT NULL,
	SDT VARCHAR(10),
	Email VARCHAR(100),
	DiaChi NVARCHAR(100) NOT NULL,
	QueQuan NVARCHAR(100) NOT NULL,
	MaTonGiao VARCHAR(4) REFERENCES TONGIAO(MaTonGiao) NOT NULL,
	MaQuocTich VARCHAR(5) REFERENCES QUOCTICH(MaQuocTich) NOT NULL
)

INSERT INTO HOCSINH VALUES('HS000001','Ton That Minh','Thanh','placeholder.png',1,'A1','PH0001','20020210','0325578154','thanh.ttm.62cntt@ntu.edu.vn','415/1 duong 2 thang 4','Dien Khanh','TG01','QT001') 
INSERT INTO HOCSINH VALUES('HS000002','Lam Phuoc','Thinh','placeholder.png',1,'A2','PH0002','20020613','0342160698','thinh.lp.62cntt@ntu.edu.vn','30/7 ha thanh','Khanh Hoa','TG02','QT002')
INSERT INTO HOCSINH VALUES('HS000003','Nguyen Van','Tuc','placeholder.png',1,'A2','PH0003','20021225','0616262442','tuc.nv.62cntt@ntu.edu.vn','231/25 duong 2 thang 4','Ninh Hoa','TG03','QT003')
INSERT INTO HOCSINH VALUES('HS000004','Dao Tuan','Nghia','placeholder.png',1,'A1','PH0004','20020702','0323415166','nghia.dt.62cntt@ntu.edu.vn','214/5 duong 2 thang 4','Dak Lak','TG04','QT004')
INSERT INTO HOCSINH VALUES('HS000005','Phan Thi Phuong','Ha','placeholder.png',0,'B1','PH0005','20020813','0253691296','ha.ptp.62cntt@ntu.edu.vn','24/1 duong 2 thang 4','Ha Noi','TG05','QT005')
INSERT INTO HOCSINH VALUES('HS000006','Le Ngoc','Tan','placeholder.png',1,'B1','PH0006','20020101','0159368268','tan.ln.62cntt@ntu.edu.vn','133/1 duong 2 thang 4','Ho Chi Minh','TG06','QT006')
INSERT INTO HOCSINH VALUES('HS000007','Tu Ngoc','Chuong','placeholder.png',1,'B2','PH0007','20020622','0441961962','chuong.tn.62cntt@ntu.edu.vn','52/7 duong 2 thang 4','Nghe An','TG07','QT007')
INSERT INTO HOCSINH VALUES('HS000008','Nguyen Quoc','Khanh','placeholder.png',1,'C1','PH0008','20020915','0619169153','khanh.nq.62cntt@ntu.edu.vn','366/5 duong 2 thang 4','Sa Pa','TG08','QT008')
INSERT INTO HOCSINH VALUES('HS000009','Dam Quan','Tuong','placeholder.png',1,'C2','PH0009','20021002','0827516816','tuong.dq.62cntt@ntu.edu.vn','314/7 duong 2 thang 4','Hue','TG09','QT009')
INSERT INTO HOCSINH VALUES('HS000010','Pham Truong','Nghiem','placeholder.png',1,'B2','PH0010','20020222','0929798170','nghiem.pt.62cntt@ntu.edu.vn','444/4 duong 2 thang 4','Nha Trang','TG01','QT001')
INSERT INTO HOCSINH VALUES('HS000011','Bui Quang','Huy','placeholder.png',1,'B1','PH0011','20020330','0978129693','huy.bq.62cntt@ntu.edu.vn','333/3 duong 2 thang 4','Nghe An','TG02','QT002')
INSERT INTO HOCSINH VALUES('HS000012','Nguyen Thai Tan','Hung','placeholder.png',1,'A2','PH0012','20020502','0182657296','hung.ntt.62cntt@ntu.edu.vn','222/2 duong 2 thang 4','Ha Noi','TG03','QT003')
INSERT INTO HOCSINH VALUES('HS000013','Le Gia','Hung','placeholder.png',1,'A2','PH0013','20020707','0192659160','hung.lg.62cntt@ntu.edu.vn','111/1 duong 2 thang 4','Thanh Hoa','TG04','QT004')
INSERT INTO HOCSINH VALUES('HS000014','Nguyen Ngoc Duc','Nhong','placeholder.png',1,'A1','PH0014','20021122','0215121717','nhong.nnd.62cntt@ntu.edu.vn','555/5 duong 2 thang 4','Sa pa','TG05','QT005')
INSERT INTO HOCSINH VALUES('HS000015','Hoang Tuan','Kiet','placeholder.png',1,'C1','PH0015','20020131','0124050060','kiet.ht.62cntt@ntu.edu.vn','122/9 duong 2 thang 4','Ninh Hoa','TG06','QT006')
INSERT INTO HOCSINH VALUES('HS000016','Nguyen Van','Dung','placeholder.png',1,'A1','PH0010','20021010','0912345678','dung.nv.62cntt@ntu.edu.vn','123/4 Nguyen Van Linh','Khanh Hoa','TG01','QT001')
INSERT INTO HOCSINH VALUES('HS000017','Tran Thi','Thu','placeholder.png',0,'A2','PH0011','20020105','0987654321','thu.tt.62cntt@ntu.edu.vn','456/7 Tran Phu','Da Nang','TG02','QT002')
INSERT INTO HOCSINH VALUES('HS000018','Le Van','Hung','placeholder.png',1,'A2','PH0012','20020320','0909090909','hung.lv.62cntt@ntu.edu.vn','789/10 Nguyen Van Cu','Quang Nam','TG03','QT003')
INSERT INTO HOCSINH VALUES('HS000019','Pham Thi','My','placeholder.png',0,'B1','PH0013','20020415','0888888888','my.pt.62cntt@ntu.edu.vn','12/3 Le Loi','Hanoi','TG04','QT004')
INSERT INTO HOCSINH VALUES('HS000020','Nguyen Van','Tien','placeholder.png',1,'B1','PH0014','20020501','0977777777','tien.nv.62cntt@ntu.edu.vn','345/6 Nguyen Trai','Khanh Hoa','TG05','QT005')
INSERT INTO HOCSINH VALUES('HS000021','Hoang Thi','Yen','placeholder.png',0,'B2','PH0015','20020616','0966666666','yen.ht.62cntt@ntu.edu.vn','567/8 Tran Hung Dao','Quang Ngai','TG06','QT006')
INSERT INTO HOCSINH VALUES('HS000022','Tran Van','Tu','placeholder.png',1,'C1','PH0016','20020711','0955555555','tu.tv.62cntt@ntu.edu.vn','789/10 Le Lai','Da Nang','TG07','QT007')
INSERT INTO HOCSINH VALUES('HS000023','Nguyen Thi','Kim','placeholder.png',0,'C1','PH0017','20020826','0944444444','kim.nt.62cntt@ntu.edu.vn','123/4 Phan Chu Trinh','Khanh Hoa','TG08','QT008')
INSERT INTO HOCSINH VALUES('HS000024','Le Van','Hai','placeholder.png',1,'C2','PH0018','20021021','0933333333','hai.lv.62cntt@ntu.edu.vn','456/7 Nguyen Dinh Chieu','Ninh Thuan','TG09','QT009')
INSERT INTO HOCSINH VALUES('HS000025','Tran Thi','Lan','placeholder.png',0,'A1','PH0019','20021206','0922222222','lan.tt.62cntt@ntu.edu.vn','789/10 Tran Phu','Quang Tri','TG09','QT010')
INSERT INTO HOCSINH VALUES('HS000026','Pham Van','Huong','placeholder.png',1,'A1','PH0020','20021201','0911111111','huong.pv.62cntt@ntu.edu.vn','12/3 Nguyen Hue','Da Nang','TG01','QT001')
INSERT INTO HOCSINH VALUES('HS000027','Nguyen Thi','Quyen','placeholder.png',0,'A2','PH0019','20020101','0900000000','quyen.nt.62cntt@ntu.edu.vn','345/6 Le Loi','Binh Dinh','TG02','QT002')
INSERT INTO HOCSINH VALUES('HS000028','Tran Van','Truong','placeholder.png',1,'B1','PH0020','20020202','0988888888','truong.tv.62cntt@ntu.edu.vn','567/8 Nguyen Trai','Quang Nam','TG03','QT003')
INSERT INTO HOCSINH VALUES('HS000029','Le Thi','Nga','placeholder.png',0,'B1','PH0013','20020303','0966666666','nga.lt.62cntt@ntu.edu.vn','789/10 Tran Hung Dao','Hanoi','TG04','QT004')
INSERT INTO HOCSINH VALUES('HS000030','Pham Van','Phuc','placeholder.png',1,'B2','PH0020','20020404','0944444444','phuc.pv.62cntt@ntu.edu.vn','123/4 Le Lai','Da Nang','TG05','QT005')

CREATE TABLE MONHOC
(
	MaMH VARCHAR(5) PRIMARY KEY,
	TenMH NVARCHAR(25) NOT NULL,
	MaNganh VARCHAR(4) REFERENCES NGANH(MaNganh) NOT NULL,
	MaKhoi VARCHAR(1) REFERENCES KHOI(MaKhoi) NOT NULL
)

INSERT INTO NGANH VALUES('STEM',N'KH tự nhiên')
INSERT INTO MONHOC VALUES('TOANA',N'Toán A','STEM','A')
INSERT INTO MONHOC VALUES('TOANB',N'Toán B','STEM','B')
INSERT INTO MONHOC VALUES('TOANC',N'Toán C','STEM','C')
INSERT INTO MONHOC VALUES('PHYSA',N'Vật lý A','STEM','A')
INSERT INTO MONHOC VALUES('PHYSB',N'Vật lý B','STEM','B')
INSERT INTO MONHOC VALUES('PHYSC',N'Vật lý C','STEM','C')
INSERT INTO MONHOC VALUES('CHEMA',N'Hoá học A','STEM','A')
INSERT INTO MONHOC VALUES('CHEMB',N'Hoá học B','STEM','B')
INSERT INTO MONHOC VALUES('CHEMC',N'Hoá học C','STEM','C')
INSERT INTO NGANH VALUES('SSCI',N'KH xã hội')
INSERT INTO MONHOC VALUES('HISTA',N'Lịch sử A','SSCI','A')
INSERT INTO MONHOC VALUES('HISTB',N'Lịch sử B','SSCI','B')
INSERT INTO MONHOC VALUES('HISTC',N'Lịch sử C','SSCI','C')
INSERT INTO MONHOC VALUES('GEOGA',N'Địa lý A','SSCI','A')
INSERT INTO MONHOC VALUES('GEOGB',N'Địa lý B','SSCI','B')
INSERT INTO MONHOC VALUES('GEOGC',N'Địa lý C','SSCI','C')
INSERT INTO MONHOC VALUES('GDCDA',N'Giáo dục công dân A','SSCI','A')
INSERT INTO MONHOC VALUES('GDCDB',N'Giáo dục công dân B','SSCI','B')
INSERT INTO MONHOC VALUES('GDCDC',N'Giáo dục công dân C','SSCI','C')

CREATE TABLE NAMHOC
(
	MaNH VARCHAR(6) PRIMARY KEY,
	NamBatDau INT NOT NULL,
	NamKetThuc INT NOT NULL,
)
INSERT INTO NAMHOC VALUES('NH0001',2021,2022)
INSERT INTO NAMHOC VALUES('NH0002',2022,2023)
INSERT INTO NAMHOC VALUES('NH0003',2023,2024)

CREATE TABLE LOAIHOCPHI
(
	MaLHP VARCHAR(4) PRIMARY KEY,
	TenLHP NVARCHAR(50) NOT NULL,
	DONGIA INT NOT NULL
)

INSERT INTO LOAIHOCPHI VALUES('LHP1','Bình thường',600000)
INSERT INTO LOAIHOCPHI VALUES('LHP2','Nghèo',300000)

CREATE TABLE HOCPHI
(
	MaHocPhi VARCHAR(10) PRIMARY KEY NOT NULL,
	MaNH VARCHAR(6) REFERENCES NAMHOC(MaNH) NOT NULL,
	MaHS VARCHAR(8) REFERENCES HOCSINH(MaHS) NOT NULL,
	MaLHP VARCHAR(4) REFERENCES LOAIHOCPHI(MaLHP) NOT NULL,
	DONGIA INT NOT NULL,
	NgayThongBao DATE NOT NULL,
	NgayDongTien DATE
)

INSERT INTO HOCPHI(MaHocPhi,MaNH,MaHS,MaLHP,DONGIA,NgayThongBao) VALUES('FE00000001','NH0001','HS000002','LHP1',600000,'20230519')

CREATE TABLE CTHP
(
	MaCTHP VARCHAR(10) PRIMARY KEY,
	MaHS VARCHAR(8) REFERENCES HOCSINH(MaHS) NOT NULL,
	MaMH VARCHAR(5) REFERENCES MONHOC(MaMH) NOT NULL,
	MaNH VARCHAR(6) REFERENCES NAMHOC(MaNH) NOT NULL,
	MaGV VARCHAR(4) REFERENCES GIAOVIEN(MaGV) NOT NULL
)

INSERT INTO CTHP VALUES('HP00000001','HS000001','TOANA','NH0002','GV01')

CREATE TABLE KETQUA
(
	MaKQ VARCHAR(10) PRIMARY KEY,
	MaCTHP VARCHAR(10) REFERENCES CTHP(MaCTHP),
	DiemQT FLOAT NOT NULL,
	Diem1T FLOAT NOT NULL,
	DiemKT FLOAT NOT NULL,
	DiemTB FLOAT NOT NULL
)

INSERT INTO KETQUA VALUES('KQ00000001','HP00000001',7,8,7,7.3)