<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="MgLinkProduct.aspx.cs" Inherits="Store.Admin.MgLinkProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <main>
        <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">إدارة الربط المنتجات
        </h1>

        <div class="addProduct w-full py-10 sm:px-10" style="direction: rtl">
            <div class="flex justify-between items-center">
                <h1 class="text-xl text-[#303841] font-black my-5">الربط المنتجات</h1>
            </div>
            <div class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">

                <!-- images -->
                <div class="p-8 rounded-md w-full">
                    <div class="mt-5">
                        <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                            <div class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:SqlDataSource ID="sqlLinkProduct" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' ProviderName="System.Data.SqlClient"
                                            DeleteCommand="DELETE FROM [LinkProduct] WHERE [Id] = @Id; DELETE FROM [InfoLinkProduct] WHERE [Id] = @Id;"
                                            SelectCommand="SELECT InfoLinkProduct.Id, Name, COUNT(*) AS Cnt FROM [InfoLinkProduct] INNER JOIN LinkProduct ON InfoLinkProduct.Id = LinkProduct.Id GROUP BY InfoLinkProduct.Id, Name">
                                            <DeleteParameters>
                                                <asp:Parameter Name="Id" DbType="Int32" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                        <asp:GridView
                                            ID="gvLinkProduct"
                                            runat="server"
                                            DataSourceID="sqlLinkProduct"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="Id"
                                            CssClass="min-w-full leading-normal" OnRowCommand="gvLinkProduct_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>

                                                <asp:BoundField DataField="Name" HeaderText="اسم الربط" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Cnt" HeaderText="عدد المنتجات المربوطة" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="إلغاء الربط" ItemStyle-CssClass="py-5 border-b border-r border-gray-200 bg-white text-sm">
                                                    <ItemTemplate>

                                                        <asp:LinkButton runat="server"
                                                            CommandName="DelLing"
                                                            CommandArgument='<%# Eval("Id") %>'>
                                                    <div class="text-center text-[18px]">
                                                    <i class="deleteBtn fa-solid fa-trash-can text-[#FF0000]"></i>
                                                </div>
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
