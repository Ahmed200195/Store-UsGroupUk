<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="PeOrder.aspx.cs" Inherits="Store.Admin.PeOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <main>
        <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">كل الطلبات
        </h1>

        <div class="addProduct w-full py-10 sm:px-10" style="direction: rtl">
            <div class="flex justify-between items-center">
                <h1 class="text-xl text-[#303841] font-black my-5">طلبات العميل</h1>
                <h2>عدد الطلبات <span id="CntOrder" runat="server">10</span></h2>
            </div>
            <div class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">

                <!-- info client -->

                <div class="grid gap-6 mb-6 md:grid-cols-2">
                    <div>
                        <label for="txtName" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">اسم العميل</label>
                        <input type="text" id="txtName" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                    <div>
                        <label for="txtPhone" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">رقم الهاتف</label>
                        <input type="text" id="txtPhone" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                </div>
                <div class="grid gap-6 mb-6 md:grid-cols-2">
                    <div>
                        <label for="txtAddress" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">اسم المنطقة</label>
                        <input type="text" id="txtAddress" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                    <div>
                        <label for="txtPiece" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">رقم القطعة</label>
                        <input type="number" id="txtPiece" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                </div>
                <div class="grid gap-6 mb-6 md:grid-cols-2">
                    <div>
                        <label for="txtHome" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">رقم المنزل</label>
                        <input type="number" id="txtHome" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                    <div>
                        <label for="txtStreet" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">رقم الشارع</label>
                        <input type="number" id="txtStreet" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" readonly="readonly" runat="server">
                    </div>
                </div>


                <!-- images -->
                <!-- start in table -->
                <div class="mt-5">
                    <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                        <div class="inline-block min-w-full shadow rounded-lg overflow-hidden">


                            <asp:SqlDataSource ID="sqlProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                                    UpdateCommand="UPDATE Client SET DateRe = GETDATE(), Received = 1 WHERE Id = @CId"
                                    SelectCommand="
                                    SELECT Product.Id, Product.[Photo], Product.NameAr as ProductName, [Dept].[NameAr] as DeptName, [Brand].[Name] as BrandName,
                                    (Total / Orders.Cnt) AS Price, Orders.Cnt, Total
                                    FROM Product
                                    INNER JOIN Orders ON Product.Id = Orders.PId
                                    INNER JOIN Brand ON Brand.Id = Product.BId
                                    INNER JOIN Dept ON Dept.Id = Product.DId
                                    WHERE Orders.CId = @CId
                                "
                                DeleteCommand="DELETE FROM Orders WHERE CId = @CId; DELETE FROM Client WHERE Id = @CId;">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="CId" QueryStringField="id" DefaultValue="0" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:QueryStringParameter Name="CId" QueryStringField="id" DefaultValue="0" />
                                </UpdateParameters>
                                <DeleteParameters>
                                    <asp:QueryStringParameter Name="CId" QueryStringField="id" DefaultValue="0" />
                                </DeleteParameters>
                                </asp:SqlDataSource>
                                <asp:GridView
                                    ID="gvProduct"
                                    runat="server"
                                    DataSourceID="sqlProduct"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="Id"
                                    CssClass="min-w-full leading-normal">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>

                                        <asp:TemplateField HeaderText="خلفية" ItemStyle-CssClass="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                            <ItemTemplate>
                                                <div class="flex items-center">
                                                    <div class="flex-shrink-0 w-24 h-20">
                                                        <img class="w-full h-full rounded lazy-load" data-src='../Uploads/Product/<%# Eval("Photo") %>' />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ProductName" HeaderText="اسم المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="DeptName" HeaderText="اسم الصنف" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="BrandName" HeaderText="الماركة" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Price" HeaderText="سعر المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Cnt" HeaderText="كمية المطلوبة" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Total" HeaderText="الإجمالي" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                        </div>

                        <div class="flex justify-between">
                            <h1 class="text-[#2185d5] font-semibold">مجموع الطلبات :<span id="totalOrder" runat="server"
                                class="font-normal text-[#3a4750]">2000</span></h1>

                            <div class="checkSend  flex items-center gap-2">
                                <button id="btnDeleteOrder" type="button" class="text-white bg-red-400 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" runat="server" onserverclick="btnDeleteOrder_ServerClick">حذف الطلب</button>
                                <button id="btnReceived" type="button" class="text-white bg-green-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" runat="server" onserverclick="btnReceived_ServerClick">استلام</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- end in table  -->
        </div>
    </main>
</asp:Content>
