<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Store.Admin.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <main
        class="dashpord flex-1 overflow-x-hidden overflow-y-auto bg-gray-200"
      >
        <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">
          لوحة التحكم
        </h1>
        <div class="container mx-auto px-6 py-8">
            <h1 class="text-xl text-[#303841] font-black my-5">العملاء</h1>
          <div class="mt-4">
            <div class="cards flex flex-wrap -mx-6 gap-y-4">
              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-indigo-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-users"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="cntClient" class="text-2xl font-semibold text-gray-700" runat="server">8,282</h4>
                    <div class="text-gray-500">عدد العملاء</div>
                  </div>
                </div>
              </div>

              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-orange-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-cart-shopping"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="cntOrders" class="text-2xl font-semibold text-gray-700" runat="server">
                      200,521
                    </h4>
                    <div class="text-gray-500">اجمالي الطلبات</div>
                  </div>
                </div>
              </div>

              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-pink-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-suitcase"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="cntSales" class="text-2xl font-semibold text-gray-700" runat="server">
                      215,542
                    </h4>
                    <div class="text-gray-500">إجمالي المبيعات</div>
                  </div>
                </div>
              </div>

              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-[#73A839] bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-dollar-sign"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="CntRecive" class="text-2xl font-semibold text-gray-700" runat="server">500 $</h4>
                    <div class="text-gray-500">مستلم</div>
                  </div>
                </div>
              </div>

                <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-[#73A839] bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-dollar-sign"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="CntUnRecive" class="text-2xl font-semibold text-gray-700" runat="server">500 $</h4>
                    <div class="text-gray-500">غير مستلم</div>
                  </div>
                </div>
              </div>
            </div>
          </div>

            <h1 class="text-xl text-[#303841] font-black my-5">المنتجات</h1>
          <div class="mt-4">
            <div class="cards flex flex-wrap -mx-6 gap-y-4">
              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-indigo-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-users"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="CntProduct" class="text-2xl font-semibold text-gray-700" runat="server">8,282</h4>
                    <div class="text-gray-500">عدد المنتجات</div>
                  </div>
                </div>
              </div>

              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-orange-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-cart-shopping"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="CntDept" class="text-2xl font-semibold text-gray-700" runat="server">
                      200,521
                    </h4>
                    <div class="text-gray-500">عدد الاصناف</div>
                  </div>
                </div>
              </div>

              <div class="w-full px-6 sm:w-1/2 xl:w-1/3">
                <div
                  class="card flex items-center px-5 py-6 shadow-sm rounded-md bg-white"
                >
                  <div
                    class="px-5 py-4 rounded-full bg-pink-600 bg-opacity-75 text-white"
                  >
                    <i class="fa-solid fa-suitcase"></i>
                  </div>

                  <div class="mx-5">
                    <h4 id="CntYesProduct" class="text-2xl font-semibold text-gray-700" runat="server">
                      215,542
                    </h4>
                    <div class="text-gray-500">المنتجات المتاحة</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
</asp:Content>
