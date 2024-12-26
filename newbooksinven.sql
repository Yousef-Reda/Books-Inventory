use books_inven;
create table book (
bid int primary key IDENTITY(1,1) ,
btitle varchar(200) not null   ,
bprice int not null ,
btype varchar(200) not null);

INSERT INTO book (btitle, bprice, btype)
VALUES 
('The Great Gatsby', 150, 'Fiction'),
('Introduction to Algorithms', 500, 'Education'),
('Harry Potter and the Sorcerer''s Stone', 300, 'Fantasy'),
('The Lean Startup', 200, 'Business'),
('To Kill a Mockingbird', 180, 'Classic');

CREATE TABLE members (
mid INT PRIMARY KEY IDENTITY(1,1),
mname VARCHAR(200) NOT NULL,
maddress VARCHAR(200),
mphone VARCHAR(50) null
);

INSERT INTO members (mname, mphone, maddress)
VALUES 
('aLi', '1234567890', 'alex'),
('yousef', '0987654321', 'cairo'),
('nour','222222', 'tanta'),
('hossam', '1122334455', 'mansoura'),
('Sara', '333333333', 'minia');

create table memberactions (
aid int primary key identity(1,1),
bid int not null ,
mid int not null ,
borrow_date date not null,
return_date date ,
foreign key(bid) references book(bid),
foreign key(mid) references members(mid)
);

INSERT INTO memberactions (bid,mid, borrow_date, return_date)
VALUES
(1,1, '2024-12-01', '2024-12-10'),  
(2,2, '2024-12-02', '2024-12-15'),
(3,3, '2024-12-03', '2024-12-12'),
(4,4, '2024-12-05', '2024-12-20'),  
(5,5, '2024-12-06', NULL);



