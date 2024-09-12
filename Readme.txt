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


[]針對新會員暫時沒做發信功能