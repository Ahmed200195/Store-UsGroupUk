<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeEmail.aspx.cs" Inherits="Store.User_Check.CodeEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Check Code</title>
    <style>
        input[type="number"] {
            display: block;
            border: none;
            padding: 0;
            width: 10.5ch;
            background: repeating-linear-gradient( 90deg, dimgrey 0, dimgrey 1ch, transparent 0, transparent 1.8ch ) 0 100% / 10ch 2px no-repeat;
            font: 4.5ch droid sans mono, consolas, monospace;
            letter-spacing: 0.8ch;
        }

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        input[type="number"]:focus {
            outline: none;
            color: dodgerblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                            <h1 class="text-2xl font-semibold capitalize">check code</h1>
                        </div>
                        <div class="divide-y divide-gray-200">
                            <div
                                class="py-8 text-base leading-6 space-y-4 text-gray-700 sm:text-lg sm:leading-7">
                                <div class="relative">
                                    <input id="txtCode" type="number" maxlength="6" runat="server" />
                                    <label
                                        for="email"
                                        class="capitalize absolute left-0 -top-3.5 text-gray-600 text-sm peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-440 peer-placeholder-shown:top-2 transition-all peer-focus:-top-3.5 peer-focus:text-gray-600 peer-focus:text-sm">
                                        enter code</label>
                                </div>
                                <div class="relative">
                                    <button id="btnSubmit" type="button" class="bg-blue-500 text-white rounded-md px-2 py-1" runat="server" onserverclick="btnSubmit_ServerClick">
                                        Submit
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div id="dvAlert" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative" role="alert" visible="false" runat="server">
                            <strong class="font-bold">Error!</strong>
                            <span class="block sm:inline">The code entered is incorrect.</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.tailwindcss.com"></script>
</body>
</html>
