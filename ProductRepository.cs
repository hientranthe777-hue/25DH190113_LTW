using System.Collections.Generic;
using System.Linq;

namespace _25DH190113_LTW.Models
{
    public static class ProductRepository
    {
        // Hàm tự động kiểm tra và nạp 4 Danh mục + 20 Nhạc cụ vào SQL Server (MyStore)
        public static void SeedDatabase(MyStoreEntities db)
        {
            // 1. Tự động thêm 4 danh mục nhạc cụ vào bảng Category nếu chưa có
            string[] tenDanhMuc = { "Guitar", "Piano", "Nhạc cụ khác", "Phụ kiện" };
            foreach (var ten in tenDanhMuc)
            {
                if (!db.Categories.Any(c => c.CategoryName == ten))
                {
                    db.Categories.Add(new Category { CategoryName = ten });
                }
            }
            db.SaveChanges();

            int idGuitar = db.Categories.First(c => c.CategoryName == "Guitar").CategoryID;
            int idPiano = db.Categories.First(c => c.CategoryName == "Piano").CategoryID;
            int idKhac = db.Categories.First(c => c.CategoryName == "Nhạc cụ khác").CategoryID;
            int idPhuKien = db.Categories.First(c => c.CategoryName == "Phụ kiện").CategoryID;

            // 2. Danh sách 20 sản phẩm nhạc cụ (10 sản phẩm gốc + 10 sản phẩm mới)
            var danhSach20SanPham = new List<Product>
            {
                new Product {
                    ProductName = "Đàn Acoustic Guitar Yamaha F310", ProductPrice = 3500000, CategoryID = idGuitar,
                    ProductDecription = "Guitar gỗ dáng khuyết, âm thanh vang, phù hợp người mới.",
                    ProductImage = "https://images.unsplash.com/photo-1516924962500-2b4b3b99ea02?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_1.mp3"
                },
                new Product {
                    ProductName = "Đàn Piano Điện Casio CDP-S110", ProductPrice = 10500000, CategoryID = idPiano,
                    ProductDecription = "Đàn piano điện nhỏ gọn, 88 phím chuẩn.",
                    ProductImage = "https://images.unsplash.com/photo-1520523839897-bd0b52f945a0?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_2.mp3"
                },
                new Product {
                    ProductName = "Trống Cơ Pearl Roadshow", ProductPrice = 12000000, CategoryID = idKhac,
                    ProductDecription = "Bộ trống cơ 5 chi tiết đầy đủ phụ kiện.",
                    ProductImage = "https://images.unsplash.com/photo-1519892300165-cb5542fb47c7?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Heater-1.mp3"
                },
                new Product {
                    ProductName = "Đàn Ukulele Soprano Mahalo", ProductPrice = 550000, CategoryID = idGuitar,
                    ProductDecription = "Ukulele size Soprano, màu gỗ tự nhiên.",
                    ProductImage = "https://images.unsplash.com/photo-1612225330812-01a9c6b355ec?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_3.mp3"
                },
                new Product {
                    ProductName = "Violin Kapok V118 Size 4/4", ProductPrice = 1800000, CategoryID = idKhac,
                    ProductDecription = "Violin tiêu chuẩn cho học sinh, sinh viên.",
                    ProductImage = "https://images.unsplash.com/photo-1612222869049-d8ec83637a3c?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Give_us_a_light.mp3"
                },
                new Product {
                    ProductName = "Đàn Electric Guitar Fender Stratocaster", ProductPrice = 25000000, CategoryID = idGuitar,
                    ProductDecription = "Guitar điện huyền thoại, âm thanh sắc nét.",
                    ProductImage = "https://images.unsplash.com/photo-1564186763535-ebb21ef5277f?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Brk_Snr.mp3"
                },
                new Product {
                    ProductName = "Đàn Grand Piano Yamaha GB1K", ProductPrice = 250000000, CategoryID = idPiano,
                    ProductDecription = "Grand piano sang trọng, âm thanh đẳng cấp.",
                    ProductImage = "https://images.unsplash.com/photo-1552422535-c45813c61732?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Bld_H1.mp3"
                },
                new Product {
                    ProductName = "Máy đập nhịp Metronome Cherub", ProductPrice = 300000, CategoryID = idPhuKien,
                    ProductDecription = "Phụ kiện đo nhịp điện tử, chính xác cao.",
                    ProductImage = "https://images.unsplash.com/photo-1470225620780-dba8ba36b745?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/side_stick_1.mp3"
                },
                new Product {
                    ProductName = "Dây đàn Acoustic Elixir 11052", ProductPrice = 450000, CategoryID = idPhuKien,
                    ProductDecription = "Dây guitar lớp phủ Nanoweb chống gỉ sét.",
                    ProductImage = "https://images.unsplash.com/photo-1516924962500-2b4b3b99ea02?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Dry_Ohh.mp3"
                },
                new Product {
                    ProductName = "Kẹp Capo Guitar Alice", ProductPrice = 80000, CategoryID = idPhuKien,
                    ProductDecription = "Capo kẹp hợp kim nhôm, lò xo chắc chắn.",
                    ProductImage = "https://images.unsplash.com/photo-1541689592655-f5f52825a3b8?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_1.mp3"
                },
                new Product {
                    ProductName = "Đàn Classic Guitar Cordoba C1M", ProductPrice = 4200000, CategoryID = idGuitar,
                    ProductDecription = "Đàn guitar cổ điển dây nylon êm tay, mặt gỗ vân sam tự nhiên.",
                    ProductImage = "https://images.unsplash.com/photo-1525201548942-d8732f6617a0?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_2.mp3"
                },
                new Product {
                    ProductName = "Đàn Acoustic Guitar Taylor Academy 10", ProductPrice = 16500000, CategoryID = idGuitar,
                    ProductDecription = "Guitar cao cấp dáng Dreadnought, âm trầm ấm áp và vang xa.",
                    ProductImage = "https://images.unsplash.com/photo-1510915361894-db8b60106cb1?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_3.mp3"
                },
                new Product {
                    ProductName = "Đàn Guitar Bass Fender Precision", ProductPrice = 19800000, CategoryID = idGuitar,
                    ProductDecription = "Guitar Bass 4 dây chuyên nghiệp cho ban nhạc sống động.",
                    ProductImage = "https://images.unsplash.com/photo-1550291652-6ea9114a47b1?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/RP4_KICK_1.mp3"
                },
                new Product {
                    ProductName = "Đàn Piano Điện Roland RP-30", ProductPrice = 16800000, CategoryID = idPiano,
                    ProductDecription = "Âm thanh SuperNATURAL Piano chân thực, tủ đàn gỗ sang trọng.",
                    ProductImage = "https://images.unsplash.com/photo-1558584673-c834fb1cc3ca?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_1.mp3"
                },
                new Product {
                    ProductName = "Đàn Piano Cơ Upright Kawai K-300", ProductPrice = 145000000, CategoryID = idPiano,
                    ProductDecription = "Piano cơ đứng Nhật Bản, bộ máy phím Millennium III siêu nhạy.",
                    ProductImage = "https://images.unsplash.com/photo-1513883049090-d0b7439799bf?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Chord_2.mp3"
                },
                new Product {
                    ProductName = "Đàn Organ Kurtzman K250", ProductPrice = 4800000, CategoryID = idPiano,
                    ProductDecription = "Organ 61 phím cảm ứng lực, tích hợp hàng trăm điệu đệm phong phú.",
                    ProductImage = "https://images.unsplash.com/photo-1598488035139-bdbb2231ce04?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Give_us_a_light.mp3"
                },
                new Product {
                    ProductName = "Bộ Trống Điện Tử Alesis Turbo Mesh Kit", ProductPrice = 8900000, CategoryID = idKhac,
                    ProductDecription = "Trống điện mặt lưới êm ái, luyện tập tại nhà với tai nghe không gây ồn.",
                    ProductImage = "https://images.unsplash.com/photo-1543443258-92b04ad5ec6b?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Heater-2.mp3"
                },
                new Product {
                    ProductName = "Kèn Saxophone Alto Yamaha YAS-280", ProductPrice = 28500000, CategoryID = idKhac,
                    ProductDecription = "Kèn Saxophone đồng vàng sang trọng, âm sắc ngọt ngào chuẩn nhạc Jazz.",
                    ProductImage = "https://images.unsplash.com/photo-1511192336575-5a79af67a629?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Heater-3.mp3"
                },
                new Product {
                    ProductName = "Trống Cajon Meinl Snarecraft", ProductPrice = 2400000, CategoryID = idKhac,
                    ProductDecription = "Trống gỗ bạch dương âm trầm ấm, dây snare nhạy cho biểu diễn mộc.",
                    ProductImage = "https://images.unsplash.com/photo-1519892300165-cb5542fb47c7?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/punchy_kick_1.mp3"
                },
                new Product {
                    ProductName = "Loa Amply Guitar Fender Frontman 10G", ProductPrice = 2100000, CategoryID = idPhuKien,
                    ProductDecription = "Loa khuếch đại 10W nhỏ gọn cho guitar điện, hỗ trợ âm thanh mộc và biến dạng.",
                    ProductImage = "https://images.unsplash.com/photo-1546868871-7041f2a55e12?q=80&w=600&auto=format&fit=crop",
                    AudioDemoUrl = "https://s3.amazonaws.com/freecodecamp/drums/Heater-4_1.mp3"
                }
            };

            foreach (var sp in danhSach20SanPham)
            {
                if (!db.Products.Any(p => p.ProductName == sp.ProductName))
                {
                    db.Products.Add(sp);
                }
            }
            db.SaveChanges();

            if (!db.Users.Any(u => u.Username == "admin"))
            {
                db.Users.Add(new User { Username = "admin", Password = "123456", UserRole = "A" });
            }
            if (!db.Users.Any(u => u.Username == "khachhang1"))
            {
                db.Users.Add(new User { Username = "khachhang1", Password = "123456", UserRole = "C" });
                db.Customers.Add(new Customer
                {
                    CustomerName = "Trần Thế Hiển",
                    CustomerPhone = "0325406579",
                    CustomerEmail = "khachhang1@gmail.com",
                    CustomerAddress = "43/21 Bình Tây, Phường Bình Tiên, TP.HCM",
                    Username = "khachhang1"
                });
            }
            db.SaveChanges();
        }
    }
}