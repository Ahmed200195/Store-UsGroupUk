<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Store.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="icon" href="../images/logoTwo.png" />
    <title>login</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sqlLogin"  ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' ProviderName="System.Data.SqlClient" runat="server"></asp:SqlDataSource>
        <div
            class="min-h-screen bg-gray-100 py-6 flex flex-col justify-center sm:py-12">
            <div class="relative py-3 sm:max-w-xl sm:mx-auto">
                <div
                    class="absolute inset-0 bg-gradient-to-r from-blue-300 to-blue-600 shadow-lg transform -skew-y-6 sm:skew-y-0 sm:-rotate-6 sm:rounded-3xl">
                </div>
                <div
                    class="relative px-4 py-10 bg-white shadow-lg sm:rounded-3xl sm:p-20">
                    <div class="max-w-md mx-auto">
                        <div>
                            <h1 class="text-2xl font-semibold capitalize">Login</h1>
                        </div>
                        <div class="divide-y divide-gray-200">
                            <div
                                class="py-8 text-base leading-6 space-y-4 text-gray-700 sm:text-lg sm:leading-7">
                                <div class="relative">
                                    <input
                                        autocomplete="off"
                                        id="txtEmail"
                                        name="email"
                                        type="text"
                                        class="peer placeholder-transparent h-10 w-full border-b-2 border-gray-300 text-gray-900 focus:outline-none focus:borer-rose-600"
                                        placeholder="Email address"
                                        runat="server" />
                                    <label
                                        for="email"
                                        class="capitalize absolute left-0 -top-3.5 text-gray-600 text-sm peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-440 peer-placeholder-shown:top-2 transition-all peer-focus:-top-3.5 peer-focus:text-gray-600 peer-focus:text-sm">
                                        Email Address</label>
                                </div>
                                <div class="relative">
                                    <input
                                        autocomplete="off"
                                        id="txtPassword"
                                        name="password"
                                        type="password"
                                        class="peer placeholder-transparent h-10 w-full border-b-2 border-gray-300 text-gray-900 focus:outline-none focus:borer-rose-600"
                                        placeholder="Password"
                                        runat="server" />
                                    <label
                                        for="password"
                                        class="capitalize absolute left-0 -top-3.5 text-gray-600 text-sm peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-440 peer-placeholder-shown:top-2 transition-all peer-focus:-top-3.5 peer-focus:text-gray-600 peer-focus:text-sm">
                                        Password</label>
                                </div>
                                <div class="relative">
                                    <button id="btnSend" class="bg-blue-500 text-white rounded-md px-2 py-1" runat="server" onserverclick="btnSend_ServerClick">
                                        send
                                    </button>
                                </div>

                            </div>
                        </div>
                        <div id="dvAlert" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative" role="alert" visible="false" runat="server">
                            <strong class="font-bold">Something went wrong!</strong>
                            <span class="block sm:inline">Email or password error</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.tailwindcss.com"></script>
</body>
</html>
