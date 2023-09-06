<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Store.Admin.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">
          تعديل معلومات
        </h1>
        <div class="setting w-full py-10 sm:px-10">
          <div class="container mx-auto px-4">
            <div
              class="grid lg:grid-cols-2 grid-cols-1 justify-center items-center gap-10 flex-wrap h-full"
            >
              <div class="text-start">
                <div class="inputSetting name">
                  <label for="">الاسم</label>
                  <div
                    class="relative flex w-full flex-wrap items-stretch my-3"
                  >
                    <span
                      class="icon z-10 h-full leading-snug font-normal text-center text-[#fff] absolute rounded-r text-base items-center justify-center w-8 py-3"
                    >
                      <i class="fa-regular fa-user"></i>
                    </span>
                    <input id="txtName" runat="server"
                      type="text"
                      class="px-10 py-3 relative bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10"
                    />
                  </div>
                </div>
                  <div class="inputSetting mt-10">
                  <label for="">البريد الكتروني</label>
                  <div
                    class="relative flex w-full flex-wrap items-stretch my-3"
                  >
                    <span
                      class="icon z-10 h-full leading-snug font-normal text-center text-[#fff] absolute rounded-r text-base items-center justify-center w-8 py-3"
                    >
                      <i class="fa-regular fa-user"></i>
                    </span>
                    <input id="txtEmail" runat="server"
                      type="email"
                      class="px-10 py-3 relative bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10"
                    />
                  </div>
                </div>
                <!-- password -->
                <div class="inputSetting password mt-10">
                  <label for="">كلمة السر</label>
                  <div
                    class="relative flex w-full flex-wrap items-stretch my-3"
                  >
                    <span
                      class="icon z-10 h-full leading-snug font-normal text-center text-[#fff] absolute rounded-r text-base items-center justify-center w-8 py-3"
                    >
                      <i class="fa-solid fa-lock"></i>
                    </span>
                    <input id="txtPswd" runat="server"
                      type="password"
                      class="px-10 py-3 relative bg-white rounded text-sm border-0 outline-none focus:outline-none focus:border-2 w-full pl-10"
                    />
                  </div>
                </div>
                <div class="update relative mt-10">
                  <button id="btnEdit"
                    class="text-white bg-[#3a4750] rounded-md px-5 py-3 capitalize space-x-2"
                      runat="server"
                     onserverclick="btnEdit_ServerClick"
                  >
                    تحديث
                  </button>
                </div>
              </div>
              <figure>
                <img
                  src="../images/admin/setting.svg"
                  alt=""
                  class="h-2/4"
                />
              </figure>
            </div>
          </div>
        </div>
</asp:Content>
