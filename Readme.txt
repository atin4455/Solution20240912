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
[V] 實作登入登出功能
只允許帳密正確且開通會員才允許登入

modify web.config, add <authentication mode="Forms">
add LoginVm, LoginDto


**安裝 AutoMapper package
add Models/MappingProfile.cs
modify Global.asax.cs, add Mapper config

modify MemberController, add Login, Logout Actions
add Login.cshtml
modify _Layout.cshtml, add Login, Logout links
modify 將About改為需登入才可檢視

modify MemberService, IMemberRepository, 新增Login相關成員

[V] 要做 Members/Index 會員中心頁，登入成功之後，導向此頁
要加[Authorize]
modify MembersController, add Index action
add Views/Members/Index.cshtml(空白範本),填入二個超連結:"修改個人基本資料","重設密碼"

[v] 實作 修改個人基本資料
modify MembersController,add EditProfile action , 要加[Authorize]
add EditProfileVm,EditProfileDto classes
    不允許修改 account,password
    增加Mapping config
add EditProfile view page

[working on] 實作 變更密碼
modify MembersController,add ChangePassword action , 要加[Authorize]
add ChangePassowrdVm,ChangePasswordDto classes
    增加Mapping config
add ChangePassword view page
modify MemberService ,add ChangePassword method

[] 針對新會員暫時沒做發佈信功能