using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using webtintuc.Models;
using webtintuc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();

//ket noi sql server:
services.AddDbContext<BlogDbContext>(option =>
    {
        option.UseSqlServer(builder.Configuration["ConnectionStrings:WebtintucContext"]);
    });
//dang ky Identity:
services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<BlogDbContext>()
        .AddDefaultTokenProviders();


//truy cap IdentityOptions
services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    // options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;
});
services.AddOptions();
var mailSetting = builder.Configuration.GetSection("MailSettings");
services.Configure<MailSettings>(mailSetting);
services.AddTransient<IEmailSender, SendMailService>();
services.AddAuthorization(option =>
{
    option.AddPolicy("AdminCP", policyBuider =>
    {
        policyBuider.RequireRole("Admin");
        policyBuider.RequireAuthenticatedUser();
    });
});

/*------------------------------------------------------------*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseStaticFiles(new StaticFileOptions()
// {
//     FileProvider = new PhysicalFileProvider(
//         Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
//     ),
//     RequestPath = "/contents"
// });

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=NewsView}/{action=Index}");

app.Run();
