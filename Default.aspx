<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en">
<head>

    <meta charset="utf-8">
    <title>Akal Academy</title>


    <!-- The styles -->
    <link id="bs-css" href="css/bootstrap-united.css" rel="stylesheet">

    <link href="https://fonts.googleapis.com/css?family=Comfortaa:400,700" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">
    <style type="text/css">
        .container > header {
            padding: 20px 30px 10px 30px;
            margin: 0px 20px 10px 20px;
            position: relative;
            display: block;
            text-shadow: 1px 1px 1px rgba(0,0,0,0.2);
            text-align: center;
        }


        body {
            background: url(SliderTheme/VisitorHomeImages/HomeBarusahib.jpg);
            background-attachment: fixed;
            background-size: cover;
            background-repeat: no-repeat;
            font-family: 'Comfortaa', cursive !important;
        }

        #wrapper {
            width: 60%;
            right: 0px;
            min-height: 560px;
            margin: 0px auto;
            width: 500px;
            position: relative;
        }


            #wrapper h1 {
                color: rgb(6, 106, 117);
                padding: 2px 0 10px 0;
                text-align: center;
                padding-bottom: 30px;
            }

                #wrapper h1:after {
                    content: ' ';
                    display: block;
                    width: 100%;
                    height: 2px;
                    margin-top: 10px;
                    background: -moz-linear-gradient(left, rgba(147,184,189,0) 0%, rgba(147,184,189,0.8) 20%, rgba(147,184,189,1) 53%, rgba(147,184,189,0.8) 79%, rgba(147,184,189,0) 100%);
                    background: -webkit-gradient(linear, left top, right top, color-stop(0%,rgba(147,184,189,0)), color-stop(20%,rgba(147,184,189,0.8)), color-stop(53%,rgba(147,184,189,1)), color-stop(79%,rgba(147,184,189,0.8)), color-stop(100%,rgba(147,184,189,0)));
                    background: -webkit-linear-gradient(left, rgba(147,184,189,0) 0%,rgba(147,184,189,0.8) 20%,rgba(147,184,189,1) 53%,rgba(147,184,189,0.8) 79%,rgba(147,184,189,0) 100%);
                    background: -o-linear-gradient(left, rgba(147,184,189,0) 0%,rgba(147,184,189,0.8) 20%,rgba(147,184,189,1) 53%,rgba(147,184,189,0.8) 79%,rgba(147,184,189,0) 100%);
                    background: -ms-linear-gradient(left, rgba(147,184,189,0) 0%,rgba(147,184,189,0.8) 20%,rgba(147,184,189,1) 53%,rgba(147,184,189,0.8) 79%,rgba(147,184,189,0) 100%);
                    background: linear-gradient(left, rgba(147,184,189,0) 0%,rgba(147,184,189,0.8) 20%,rgba(147,184,189,1) 53%,rgba(147,184,189,0.8) 79%,rgba(147,184,189,0) 100%);
                }


            #wrapper input:not([type="checkbox"]) {
                width: 92%;
                margin-top: 4px;
                padding: 10px 5px 10px;
                border: 1px solid rgb(178, 178, 178);
                -webkit-appearance: textfield;
                -webkit-box-sizing: content-box;
                -moz-box-sizing: content-box;
                box-sizing: content-box;
                -webkit-border-radius: 3px;
                -moz-border-radius: 3px;
                border-radius: 3px;
                -webkit-box-shadow: 0px 1px 4px 0px rgba(168, 168, 168, 0.6) inset;
                -moz-box-shadow: 0px 1px 4px 0px rgba(168, 168, 168, 0.6) inset;
                box-shadow: 0px 1px 4px 0px rgba(168, 168, 168, 0.6) inset;
                -webkit-transition: all 0.2s linear;
                -moz-transition: all 0.2s linear;
                -o-transition: all 0.2s linear;
                transition: all 0.2s linear;
            }


        p.change_link {
            position: absolute;
            color: rgb(127, 124, 124);
            left: 0px;
            height: 20px;
            width: 438px;
            padding: 17px 30px 20px 30px;
            font-size: 16px;
            text-align: right;
            border-top: 1px solid rgb(219, 229, 232);
            -webkit-border-radius: 0 0 5px 5px;
            -moz-border-radius: 0 0 5px 5px;
            border-radius: 0 0 5px 5px;
            background: rgb(225, 234, 235);
            background: -moz-repeating-linear-gradient(-45deg, rgb(247, 247, 247), rgb(247, 247, 247) 15px, rgb(225, 234, 235) 15px, rgb(225, 234, 235) 30px, rgb(247, 247, 247) 30px );
            background: -webkit-repeating-linear-gradient(-45deg, rgb(247, 247, 247), rgb(247, 247, 247) 15px, rgb(225, 234, 235) 15px, rgb(225, 234, 235) 30px, rgb(247, 247, 247) 30px );
            background: -o-repeating-linear-gradient(-45deg, rgb(247, 247, 247), rgb(247, 247, 247) 15px, rgb(225, 234, 235) 15px, rgb(225, 234, 235) 30px, rgb(247, 247, 247) 30px );
            background: repeating-linear-gradient(-45deg, rgb(247, 247, 247), rgb(247, 247, 247) 15px, rgb(225, 234, 235) 15px, rgb(225, 234, 235) 30px, rgb(247, 247, 247) 30px );
        }

        #wrapper p.change_link a {
            display: inline-block;
            font-weight: bold;
            background: rgb(247, 248, 241);
            padding: 2px 6px;
            color: rgb(29, 162, 193);
            margin-left: 10px;
            text-decoration: none;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            border: 1px solid rgb(203, 213, 214);
            -webkit-transition: all 0.4s linear;
            -moz-transition: all 0.4s linear;
            -o-transition: all 0.4s linear;
            -ms-transition: all 0.4s linear;
            transition: all 0.4s linear;
        }

            #wrapper p.change_link a:hover {
                color: rgb(57, 191, 215);
                background: rgb(247, 247, 247);
                border: 1px solid rgb(74, 179, 198);
            }

        #login {
            position: absolute;
            top: 0px;
            width: 88%;
            padding: 18px 6% 60px 6%;
            margin: 0 35px;
            background: rgb(247, 247, 247);
            border: 1px solid rgba(147, 184, 189,0.8);
            -webkit-box-shadow: 0pt 2px 5px rgba(105, 108, 109, 0.7), 0px 0px 8px 5px rgba(208, 223, 226, 0.4) inset;
            -moz-box-shadow: 0pt 2px 5px rgba(105, 108, 109, 0.7), 0px 0px 8px 5px rgba(208, 223, 226, 0.4) inset;
            box-shadow: 0pt 2px 5px rgba(105, 108, 109, 0.7), 0px 0px 8px 5px rgba(208, 223, 226, 0.4) inset;
            -webkit-box-shadow: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

        .animated {
            -webkit-animation-fill-mode: both;
            -moz-animation-fill-mode: both;
            -ms-animation-fill-mode: both;
            -o-animation-fill-mode: both;
            animation-fill-mode: both;
            -webkit-animation-duration: 1s;
            -moz-animation-duration: 1s;
            -ms-animation-duration: 1s;
            -o-animation-duration: 1s;
            animation-duration: 1s;
        }
    </style>
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/charisma-app.css" rel="stylesheet">
    <link href="css/jquery-ui-1.8.21.custom.css" rel="stylesheet">
    <link href='css/fullcalendar.css' rel='stylesheet'>
    <link href='css/fullcalendar.print.css' rel='stylesheet' media='print'>
    <link href='css/chosen.css' rel='stylesheet'>
    <link href='css/uniform.default.css' rel='stylesheet'>
    <link href='css/colorbox.css' rel='stylesheet'>
    <link href='css/jquery.cleditor.css' rel='stylesheet'>
    <link href='css/jquery.noty.css' rel='stylesheet'>
    <link href='css/noty_theme_default.css' rel='stylesheet'>
    <link href='css/elfinder.min.css' rel='stylesheet'>
    <link href='css/elfinder.theme.css' rel='stylesheet'>
    <link href='css/jquery.iphone.toggle.css' rel='stylesheet'>
    <link href='css/opa-icons.css' rel='stylesheet'>
    <link href='css/uploadify.css' rel='stylesheet'>


    <link rel="shortcut icon" href="img/favicon.ico">

    <%-- slider--%>
    <link rel="stylesheet" href="themes/default/default.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="SliderTheme/SliderJs/nivo-slider.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="style.css" type="text/css" media="screen" />
    <script src="js/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="SliderTheme/SliderJs/jquery.nivo.slider.js"></script>
    <script type="text/javascript">

        //<!--  Load the slider  --> 
        $(window).load(function () {
            $('#slider').nivoSlider();
        });

    </script>
</head>

<body>
    <div class="container" style="width: 100%">

        <header>
            <marquee behavior='scroll' direction='left'><h1><span style="color:rgb(255,255,255); text-shadow: 2px 6px 10px #000; font-size:36px; font-family: 'Comfortaa', cursive;font-weight:700;">THE KALGIDHAR TRUST BARU SAHIB, DIST. SIRMOUR HP-173101</span></h1></marquee>
            <h1><span style="color: rgb(255,255,255); font-size: 30px; text-shadow: 2px 2px 8px #000; font-family: 'Montserrat', sans-serif;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kalgidhar Trust Management System</span></h1>
        </header>
        <section>
            <div id="container_demo">
                <a class="hiddenanchor" id="toregister"></a>
                <a class="hiddenanchor" id="tologin"></a>
                <div id="wrapper">
                    <div id="login" class="animate form">
                        <form id="Form1" class="form-horizontal" runat="server">
                            <h1>Log in</h1>

                            <p>
                                <label for="username">Your User Name </label>
                                <span class="add-on"><i class="icon-user"></i></span>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="input-large span10" placeholder="myusername or mymail@mail.com"></asp:TextBox>
                            </p>
                            <p>
                                <label for="password">Your Password </label>
                                <span class="add-on"><i class="icon-lock"></i></span>
                                <asp:TextBox ID="txtPwd" runat="server" CssClass="input-large span10" TextMode="Password"></asp:TextBox>
                            </p>
                            <p>
                                <label class="remember" for="remember">
                                    <asp:CheckBox runat="server" ID="chkRemMe" />&nbsp;&nbsp;&nbsp;Remember Me</label><br />
                            </p>
                            <p>
                                <a href="http://cookingfoodsworld.blogspot.in/" target="_blank"></a>
                            </p>
                            <p class="change_link">
                                <asp:Button runat="server" Text="Login" Style="display: inline-block; font-weight: bold; font-family: 'Comfortaa', cursive; font-size: 16px; background: rgb(247, 248, 241); padding: 0px 10px; color: rgb(29, 162, 193); margin-left: 10px; text-decoration: none; border-radius: 4px; border: 1px solid rgb(203, 213, 214); height: 22px; width: 50px;" ID="btnLogin" OnClick="btnLogin_Click" />
                                <asp:Button runat="server" Text="Forget Password" Style="display: inline-block; font-weight: bold; font-family: 'Comfortaa', cursive; font-size: 16px; background: rgb(247, 248, 241); padding: 0px 10px; color: rgb(29, 162, 193); margin-left: 10px; text-decoration: none; border-radius: 4px; border: 1px solid rgb(203, 213, 214); height: 22px; width: 150px;" ID="btnForgetPassword" OnClick="btnForgetPassword_Click" />
                            </p>
                        </form>
                    </div>
                </div>
            </div>
            <div style="margin-top: -160px; margin-left: 32px; float: left;">
                <h1><span style="color: rgb(255,255,255); font-size: 25px; text-shadow: 2px 6px 12px #000; font-family: 'Montserrat', sans-serif;">Download AkalSewa App</span></h1>

                <a href="https://www.akalsewa.org/Android/uploads/AkalSewa_V1.0_1.apk" target='_blank'>
                    <img src="img/android.png"  style="width: 300px;"/></a>
            </div>
        </section>
</body>
</html>
