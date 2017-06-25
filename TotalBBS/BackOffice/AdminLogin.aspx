<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" EnableViewState="false" ClientIDMode="Static" Inherits="TotalBBS.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Page</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
		
	<!-- #CSS Links -->
	<!-- Basic Styles -->
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/font-awesome.min.css">

	<!-- SmartAdmin Styles : Caution! DO NOT change the order -->
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/smartadmin-production-plugins.min.css">
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/smartadmin-production.min.css">
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/smartadmin-skins.min.css">

	<!-- SmartAdmin RTL Support -->
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/smartadmin-rtl.min.css"> 

	<!-- Demo purpose only: goes with demo.js, you can delete this css when designing your own WebApp -->
	<link rel="stylesheet" type="text/css" media="screen" href="/App_Themes/CSS/demo.min.css">

	<!-- #GOOGLE FONT -->
	<link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">
    <script type="text/javascript">
        <!--
        $document.ready(function () {
            if (parent.frames.length != 0) {
                alert('로그아웃 되어 로그인 페이지로 이동됩니다.');
                top.location = "/AdminLogin.aspx";
            }
            $("TxtLoginId").focus();
        });

        function fnLoginInfo() {
            if ($.trim($("#TxtLoginId").val()) == "") {
                alert('로그인 아이디를 입력하세요.');
                $("#TxtLoginId").focus();
                return false;
            }

            if ($.trim($("#TxtLoginPw").val()) == "") {
                alert('로그인 비밀번호를 입력하세요.');
                $("#TxtLoginPw").focus();
                return false;
            }

            return true;
        }
        //-->
    </script>
</head>
<body class="animated fadeInDown">
    <header id="header">
	    <div id="logo-group">
		    <span id="logo"> BackOffice </span>
	    </div>
    </header>

	<div id="main" role="main">
		<!-- MAIN CONTENT -->
		<div id="content" class="container">

			<div class="row">
				<div class="col-xs-12 col-sm-12 col-md-7 col-lg-8 hidden-xs hidden-sm">
					<h1 class="txt-color-red login-header-big">BackOffice</h1>
				</div>
				<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">
					<div class="well no-padding">
                        <form id="form2" runat="server" method="post" class="smart-form client-form">
							<header>
								Sign In
							</header>

							<fieldset>
								<section>
									<label class="label">Id</label>
									<label class="input"> <i class="icon-append fa fa-user"></i>
                                        <input type="text" id="TxtLoginId" maxlength="100" class="userid" onkeydown="return (event.keyCode!=13);" runat="server"/>
										<b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-teal"></i> Please enter email address/username</b></label>
								</section>

								<section>
									<label class="label">Password</label>
									<label class="input"> <i class="icon-append fa fa-lock"></i>
                                        <input type="password" id="TxtLoginPw" maxlength="50" class="userpw" runat="server"/>
										<b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your password</b> </label>
								</section>

								<section>
									<label class="checkbox">
										<input type="checkbox" name="remember" checked="">
										<i></i>Stay signed in</label>
								</section>
							</fieldset>
							<footer>
                                <asp:Button ID="btn_login" class="btn btn-primary" OnClientClick="return fnLoginInfo();" onclick="btnLogin_Click" runat="server" AlternateText="Login" Text="Sign in"></asp:Button>
							</footer>
						</form>

					</div>
						
				</div>
			</div>
		</div>
	</div>

	<!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
	<script src="/Common/js/libs/jquery-2.1.1.min.js"></script>
	<script src="/Common/js/libs/jquery-ui-1.10.3.min.js"></script>

	<!-- IMPORTANT: APP CONFIG -->
	<script src="/Common/js/app.config.js"></script>

	<!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
	<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

	<!-- BOOTSTRAP JS -->		
	<script src="/Common/js/bootstrap/bootstrap.min.js"></script>

	<!-- JQUERY VALIDATE -->
	<script src="/Common/js/plugin/jquery-validate/jquery.validate.min.js"></script>
		
	<!-- JQUERY MASKED INPUT -->
	<script src="/Common/js/plugin/masked-input/jquery.maskedinput.min.js"></script>
		
	<!--[if IE 8]>
		<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
	<![endif]-->

	<!-- MAIN APP JS FILE -->
	<script src="/Common/js/app.min.js"></script>
</body>
</html>
