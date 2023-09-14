<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/ControlPanel.Master" AutoEventWireup="true" CodeBehind="MgItem.aspx.cs" Inherits="Store.Admin.MgItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <h1 class="text-white text-2xl bg-[#2185d5] p-3 relative z-10 w-full">الاصناف
            </h1>
            <div
                class="container displayCategory relative z-10 w-full py-10 sm:px-12 px-5"
                style="direction: rtl">
                <h1 class="text-xl text-[#303841] font-black my-5">اضافة الاصناف</h1>
                <div
                    class="boxCategory relative bg-white border-2 border-t-[#2185d5] w-full p-5 rounded-md">
                    <div class="flex flex-wrap justify-center gap-10 mt-5">
                        <div class="flex flex-col justify-center gap-10">
                            <div>
                                <label for="">اسم الصنف (عربي)</label>
                                <input id="txtNameAr" type="text" class="border-2 rounded p-2 w-96" runat="server" />
                            </div>
                            <div>
                                <label for="">اسم الصنف (انكليزي)</label>
                                <input id="txtNameEn" type="text" class="border-2 rounded p-2 w-96 text-left" runat="server" />
                            </div>
                            <div class="">
                                <label for="inputFile" class="">صورة الصنف</label>
                                <asp:FileUpload ID="inputFile" CssClass="border-2 rounded p-2 w-96" runat="server" accept=".png,.jpg,.jpeg,.gif" onchange="loadFile(event)" />
                            </div>
                            <%--<div id="alertError" class="hidden fixed bottom-3 left-3 max-w-fit flex rounded-lg border-l-[6px] border-[#F87171] bg-[#F87171] bg-opacity-[15%] px-7 py-8 shadow-md">
                                <div
                                    class="me-5 flex h-9 w-full max-w-[36px] items-center justify-center rounded-lg bg-[#F87171] text-[#fff]">
                                    <i class="fa-solid fa-triangle-exclamation"></i>
                                </div>
                                <div class="w-full">
                                    <h5 class="text-base font-semibold text-[#B45454]">يرجى ملئ الحقول فارغة</h5>
                                </div>
                            </div>--%>
                            <!-- add to table -->
                            <button
                                id="btnAddDept"
                                type="button"
                                class="bg-[#0074D9] text-white rounded-md px-5 py-3 capitalize cursor-pointer"
                                runat="server"
                                onserverclick="btnAddDept_ServerClick">
                                اضافة
                            </button>
                        </div>

                        <!-- choose image -->
                        <div class="w-96 h-auto">
                            <img
                                id="imgView"
                                src="../images/admin/imageEmpty.png"
                                alt=""
                                class="placeImg bg-[#f8f9fe] w-full"
                                runat="server"/>
                        </div>
                    </div>
                </div>

                <!-- start in table -->
                <div class="rounded-md w-full mt-10">
                    <h1 class="text-xl text-[#303841] font-black">عرض الاصناف</h1>
                    <div class="relative mt-4">
                        <span>عدد الاصناف (<span id="cntDept" runat="server"></span>)</span>
                    </div>
                    <div class="mt-2">
                        <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                            <div
                                class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                                <asp:SqlDataSource ID="sqlDept" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' ProviderName="System.Data.SqlClient"
                                    DeleteCommand="DELETE FROM [Dept] WHERE [Id] = @Id"
                                    InsertCommand="INSERT INTO [Dept] ([NameAr], [NameEn], [Photo]) VALUES (@NameAr, @NameEn, @Photo)"
                                    SelectCommand="SELECT * FROM [Dept]"
                                    UpdateCommand="UPDATE [Dept] SET [NameAr] = @NameAr, [NameEn] = @NameEn, [Photo] = @Photo WHERE [Id] = @Id"
                                    OnInserting="sqlDept_Inserting"
                                    OnUpdating="sqlDept_Updating">
                                    <InsertParameters>
                                        <asp:Parameter Name="NameAr" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="NameEn" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="Photo" Type="Object"></asp:Parameter>
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="NameAr" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="NameEn" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="Photo" Type="Object"></asp:Parameter>
                                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                                <asp:GridView
                                    ID="gvDept"
                                    runat="server"
                                    DataSourceID="sqlDept"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="Id"
                                    CssClass="min-w-full leading-normal"
                                    OnRowCommand="gvDept_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>
                                        <asp:TemplateField HeaderText="الصورة" ItemStyle-CssClass="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                            <ItemTemplate>
                                                <div class="flex items-center">
                                                    <div class="flex-shrink-0 w-24 h-20">
                                                        <img class="w-full h-full rounded" src='data:image;base64,<%# Convert.ToBase64String((byte[])Eval("Photo")) %>' />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="NameAr" HeaderText="اسم الصنف (عربي)" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
                                            <HeaderStyle CssClass="px-5 py-3 border-b-2 border-gray-200 text-right text-xs font-semibold uppercase tracking-wider bg-gray-700 text-white" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NameEn" HeaderText="اسم الصنف (انكليزي)" ItemStyle-CssClass="px-5 py-5 border-b border-r border-gray-200 bg-white text-sm" SortExpression="Name">
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
                    </div>
                </div>
                <!-- end in table -->
                <!-- start no data -->
                <div id="dvNoData" class="noData flex flex-col justify-center items-center" runat="server" visible="false">
                    <img
                        src="../images/admin/noData.svg"
                        class="w-full md:w-2/4 h-96" />
                    <div class="flex flex-col justify-center items-center mt-5">
                        <h1 class="capitalize text-2xl text-center font-black">لا توجد اصناف لعرضها
                        </h1>
                    </div>
                </div>
                <!-- end no data -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddDept" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function hideAlertDiv(id) {
            document.getElementById(id).classList.remove('hidden');
            setTimeout(function () {
                document.getElementById(id).classList.add('hidden');
            }, 3500)
        }
        var loadFile = function (event) {
            var output = document.getElementById('<%= imgView.ClientID %>');
            output.src = URL.createObjectURL(event.target.files[0]);
            output.onload = function () {
                URL.revokeObjectURL(output.src) // free memory
            }
        };
    </script>
</asp:Content>
