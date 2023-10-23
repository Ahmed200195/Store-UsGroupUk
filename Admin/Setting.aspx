<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Store.Admin.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">تعديل معلومات
    </h1>
    <div class="setting w-full py-10 sm:px-10">
        <div class="container mx-auto px-4">
            <div
                class="grid lg:grid-cols-2 grid-cols-1 justify-center items-center gap-10 flex-wrap h-full">
                <div class="text-start">
                    <div class="inputSetting name">
                        <label for="">الاسم</label>
                        <div
                            class="relative flex w-full flex-wrap items-stretch my-3">

                            <span
                                class="icon z-10 h-full leading-snug font-normal text-center text-[#888] absolute rounded-r text-base items-center justify-center w-8 py-3">
                                <i class="fa-solid fa-user"></i>
                            </span>
                            <input id="txtName" runat="server"
                                type="text"
                                class="px-10 py-3 relative text-center bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10" />
                        </div>
                    </div>
                    <div class="inputSetting mt-10">
                        <label for="">البريد الكتروني</label>
                        <div
                            class="relative flex w-full flex-wrap items-stretch my-3">
                            <span
                                class="icon z-10 h-full leading-snug font-normal text-center text-[#888] absolute rounded-r text-base items-center justify-center w-8 py-3">
                                <i class="fa-solid fa-envelope"></i>
                            </span>
                            <input id="txtEmail" runat="server"
                                type="email"
                                class="px-10 py-3 relative text-center bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10" />
                        </div>
                    </div>
                    <!-- password -->
                    <div class="inputSetting password mt-10">
                        <label for="">كلمة السر</label>
                        <div
                            class="relative flex w-full flex-wrap items-stretch my-3">
                            <span
                                class="icon z-10 h-full leading-snug font-normal text-center text-[#888] absolute rounded-r text-base items-center justify-center w-8 py-3">
                                <i class="fa-solid fa-lock"></i>
                            </span>
                            <input id="txtPswd" runat="server"
                                type="text"
                                class="px-10 py-3 relative text-center bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10" />
                        </div>
                    </div>
                    <!-- Sender +96566652783 -->
                    <div class="inputSetting mt-10">
                        
                        <label for="">تفاصيل بوابة الواتساب</label>
                        <br />
                        <br />
                        <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
                            <table class="w-full text-sm text-right text-gray-500 dark:text-gray-400">
                                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                                    <tr>
                                        <th scope="col" class="px-6 py-3">الوصف
                                        </th>
                                        <th scope="col" class="px-6 py-3">الحالة
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                        <td class="px-6 py-4">المرسل
                                        </td>
                                         <td class="px-6 py-4" dir="ltr">
                                             <a href="https://api.textmebot.com/addphone.php?apikey=EsnpvQrmfwXG" class="font-medium text-blue-600 dark:text-blue-500 hover:underline mr-4" target="_blank"><i class="fa-solid fa-share"></i></a>
                                             <span id="tdSender" runat="server"></span>
                                        </td>
                                    </tr>
                                    <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                        <td class="px-6 py-4">الاشتراك
                                        </td>
                                         <td class="px-6 py-4" dir="ltr">
                                             <a href="https://textmebot.com/#prices" class="font-medium text-blue-600 dark:text-blue-500 hover:underline mr-4" target="_blank"><i class="fa-solid fa-share"></i></a>
                                             <span id="tdSubscription" runat="server"></span>
                                        </td>
                                    </tr>
                                    <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                                        <td class="px-6 py-4">الاتصال QR Code
                                        </td>
                                         <td class="px-6 py-4" dir="ltr">
                                             <a href="https://api.textmebot.com/status.php?apikey=EsnpvQrmfwXG" class="font-medium text-blue-600 dark:text-blue-500 hover:underline mr-4" target="_blank"><i class="fa-solid fa-share"></i></a>
                                             <span id="tdDbStatus" runat="server"></span>
                                        </td>
                                    </tr>
                                    <tr class="bg-white dark:bg-gray-800 hover:bg-gray-50 dark:hover:bg-gray-600">
                                        <td class="px-6 py-4">مفتاح API
                                        </td>
                                         <td class="px-6 py-4">EsnpvQrmfwXG
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- Receiver -->
                    <div class="inputSetting mt-10">
                        <label for="">مستقبل</label>
                        <div
                            class="relative flex w-full flex-wrap items-stretch my-3">
                            <span
                                class="icon z-10 h-full leading-snug font-normal text-center text-[#888] absolute rounded-r text-base items-center justify-center w-8 py-3">
                                <i class="fa-brands fa-square-whatsapp"></i>
                            </span>
                            <input id="txtReciever" runat="server" dir="ltr"
                                type="text"
                                class="px-10 py-3 relative text-center bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10" />
                        </div>
                    </div>
                    <div class="update relative mt-10">
                        <button id="btnEdit"
                            class="text-white bg-[#3a4750] rounded-md px-5 py-3 capitalize space-x-2"
                            runat="server"
                            onserverclick="btnEdit_ServerClick">
                            تحديث
                        </button>
                    </div>
                </div>
                <figure>
                    <img
                        src="../images/admin/setting.svg"
                        alt=""
                        class="h-2/4" />
                </figure>
            </div>
        </div>
    </div>
</asp:Content>
