اجرای پروزه از طربق نرم افزار 
Visual Studio 
است
دیتا بیس در 
sql server 2022 
ساخنه شده است.
----------------------------------------------------------------------------------------------
تنظیم Connection String در appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\DBSQL;Database=Tiket;Trusted_Connection=True;TrustServerCertificate=True"
}
----------------------------------------------------------------------------------------------
Seed اولیه

یک Admin:

Email: admin@test.com

Password: 123456

یک Employee:

Email: employee@test.com

Password: 123456

این کاربران هنگام اجرای پروژه به صورت خودکار ایجاد می‌شوند.
----------------------------------------------------------------------------------------------
Endpoints

POST /auth/login – ورود و دریافت JWT

GET /tickets – لیست تمام تیکت‌ها (Admin)

GET /tickets/my – لیست تیکت‌های کاربر فعلی (Employee)

POST /tickets – ایجاد تیکت (Employee)

PUT /tickets/{id} – به‌روزرسانی وضعیت و تخصیص (Admin)

DELETE /tickets/{id} – حذف تیکت (Admin)

GET /tickets/stats – آمار تیکت‌ها (Admin)
----------------------------------------------------------------------------------------------
فایل پست من و همینطور بکاپ دیتابیس در کنار فایل 
README.txt 
موجود است