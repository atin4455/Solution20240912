[V]建立EFModels
add/Models/EFModels/folder
建立AppDbContext,Connection String,Entity Classes
---------------
會員系統
[working on]add 註冊新會員
add/Models/Infra/HashUtility.cs
add AppSetting,key=....

[working on]實作註冊功能
add RegisterVm
add 擴充方法 ToMember(RegisterVm)
add MembersController,Register Action.cshtml(不必寫 action)
modify _Layout.cshtml,add Register link

[]針對新會員暫時沒做發信功能