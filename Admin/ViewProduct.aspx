<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="ViewProduct.aspx.cs" Inherits="Store.Admin.ViewProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">عرض المنتج
    </h1>
    <div
        class="displayProduct w-full py-10 container sm:px-10 bg-[#f8f9fe]"
        style="direction: rtl">
        <h1 class="titleMain text-2xl text-right">اضافة</h1>
        <!-- start in table  -->
        <div class="p-8 rounded-md w-full">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div
                        class="flex md:flex-row flex-col justify-center items-center md:justify-between">
                        <div class="relative">
                            <span>عدد المنتجات</span>&nbsp<span id="cntProduct" runat="server">(2)</span>
                        </div>
                        <!-- select -->
                        <div class="relative h-10 w-72 min-w-[200px] md:mt-0 mt-3">
                            <label for="ddlDept" class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none text-[11px] font-normal leading-tight text-gray-500 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-gray-500 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-gray-500 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[3.75] peer-placeholder-shown:text-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-[#0074D9] peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:border-[#0074D9] peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:border-[#0074D9] peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-gray-500">اختيار الصنف</label>
                            <asp:DropDownList ID="ddlDept" AutoPostBack="true" CssClass="peer h-full w-full rounded-[7px] border border-gray-500 border-t-transparent bg-transparent px-3 py-2.5 font-sans text-sm font-normal text-gray-500 outline outline-0 transition-all placeholder-shown:border placeholder-shown:border-gray-500 placeholder-shown:border-t-gray-500 empty:!bg-red-500 focus:border-2 focus:border-[#0074D9] focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-gray-500" runat="server" DataSourceID="sqlDept" DataTextField="NameAr" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlDept" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT [Id], [NameAr] FROM [Dept]" />
                        </div>
                    </div>
                    <!-- start in table -->
                    <div class="mt-5">
                        <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                            <div
                                class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                                <asp:SqlDataSource ID="sqlProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>'
                                    DeleteCommand="DELETE FROM [Product] WHERE [Id] = @Id"
                                    SelectCommand="SELECT [Product].[Id], [NameAr], [Price], [Discount], [Brand].[Name] as BrandName, [DId], ISNULL([Photo],'x') as [Photo], [Color].[Name] as ColroName FROM [Product] INNER JOIN Brand ON Brand.Id = BId INNER JOIN Color ON Color.Id = CId WHERE ([DId] = @DId) ORDER BY [Id] DESC">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlDept" PropertyName="SelectedValue" Name="DId" Type="Int32"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:GridView
                                    ID="gvProduct"
                                    runat="server"
                                    DataSourceID="sqlProduct"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="Id"
                                    CssClass="min-w-full leading-normal"
                                    OnRowCommand="gvProduct_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>

                                        <asp:TemplateField HeaderText="خلفية" ItemStyle-CssClass="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                            <ItemTemplate>
                                                <div class="flex items-center">
                                                    <div class="flex-shrink-0 w-24 h-20">
                                                        <img class="w-full h-full rounded" src='data:image;base64,<%# Convert.ToBase64String((byte[])Eval("Photo")) %>' />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="NameAr" HeaderText="اسم المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Price" HeaderText="سعر المنتج" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Discount" HeaderText="سعر بعد الخصم" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="ColroName" HeaderText="اللون" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="BrandName" HeaderText="الماركة" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="تعديل" ItemStyle-CssClass="py-5 border-b border-r border-gray-200 bg-white text-sm">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="JobIdLink" runat="server"
                                                    CommandName="EditDept"
                                                    CommandArgument='<%# Eval("Id") %>' >
                                                    <div class="text-center text-[18px]">
                                                    <i class="fa-solid fa-pen-to-square text-[#FFA500]" runat="server"></i>
                                                </div>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- end in table  -->
    <!-- start no data -->
    <div id="dvNotData" class="noData flex flex-col justify-center items-center" runat="server" visible="false">
        <img src="/images/admin/noData.svg" class="w-2/4 h-96" />
        <div class="flex flex-col justify-center items-center mt-5">
            <h1 class="capitalize text-2xl text-center font-black">لا توجد منتجات لعرضها
            </h1>
            <p class="capitalize mt-3">لاضافة المنتجات يرجى الضغط</p>
            <a
                href="MgProduct.aspx"
                class="bg-[#0074D9] mt-3 text-white rounded-md px-5 py-3 capitalize space-x-2 cursor-pointer">اضافة بيانات</a>
        </div>
    </div>
    <!-- end no data -->
</asp:Content>
