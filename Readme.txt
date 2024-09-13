[V]建立EFModels
add/Models/EFModels/folder
建立AppDbContext,Connection String,Entity Classes
---------------
會員系統
[V]add 註冊新會員
add/Models/Infra/HashUtility.cs
add AppSetting,key=....

[V]實作註冊功能
add RegisterVm
add 擴充方法 ToMember(RegisterVm)
add MembersController,Register Action.cshtml(不必寫 action)
modify _Layout.cshtml,add Register link

[working on]實作 新會員 Email 確認功能
    信裡的網址，為 https://localhost:44352/Members/ActiveRegister?memberId=2&confirmCode=9f5eac08c2f6437b8aa30df878e58935
    modify MemberController, add ActiveRegister Action
        update isConfirmed=1, confirmCode = null
    add ActiveRegister.cshtml

//第一天進度
[working on]實作登入登出功能
modify web.config,add<authentication mode="Forms">
add LoginVm, LoginDto
modify MembersController, add Login, Logout Actions
add Login.cshtml

** 安裝 AutoMapper package
   add Models/MappingProfile.cs
   modify Global.asax.cs, add Mapper config


modify _Layout.cshtml, add Login ,Logout links
modify 將 About 改成需要登入才能檢視

[]要做 Members/Index 會員中心頁，登入成功之後，導向此頁
[]針對新會員暫時沒做發信功能