Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Globalization
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Security

Namespace DevExpressMvcApplication_Menu.Models

	#Region "Models"
	<PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage := "The new password and confirmation password do not match.")> _
	Public Class ChangePasswordModel
		Private privateOldPassword As String
		<Required, DataType(DataType.Password), DisplayName("Current password")> _
		Public Property OldPassword() As String
			Get
				Return privateOldPassword
			End Get
			Set(ByVal value As String)
				privateOldPassword = value
			End Set
		End Property

		Private privateNewPassword As String
		<Required, ValidatePasswordLength, DataType(DataType.Password), DisplayName("New password")> _
		Public Property NewPassword() As String
			Get
				Return privateNewPassword
			End Get
			Set(ByVal value As String)
				privateNewPassword = value
			End Set
		End Property

		Private privateConfirmPassword As String
		<Required, DataType(DataType.Password), DisplayName("Confirm new password")> _
		Public Property ConfirmPassword() As String
			Get
				Return privateConfirmPassword
			End Get
			Set(ByVal value As String)
				privateConfirmPassword = value
			End Set
		End Property
	End Class

	Public Class LogOnModel
		Private privateUserName As String
		<Required, DisplayName("User name")> _
		Public Property UserName() As String
			Get
				Return privateUserName
			End Get
			Set(ByVal value As String)
				privateUserName = value
			End Set
		End Property

		Private privatePassword As String
		<Required, DataType(DataType.Password), DisplayName("Password")> _
		Public Property Password() As String
			Get
				Return privatePassword
			End Get
			Set(ByVal value As String)
				privatePassword = value
			End Set
		End Property

		Private privateRememberMe As Boolean
		<DisplayName("Remember me?")> _
		Public Property RememberMe() As Boolean
			Get
				Return privateRememberMe
			End Get
			Set(ByVal value As Boolean)
				privateRememberMe = value
			End Set
		End Property
	End Class

	<PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage := "The password and confirmation password do not match.")> _
	Public Class RegisterModel
		Private privateUserName As String
		<Required, DisplayName("User name")> _
		Public Property UserName() As String
			Get
				Return privateUserName
			End Get
			Set(ByVal value As String)
				privateUserName = value
			End Set
		End Property

		Private privateEmail As String
		<Required, DataType(DataType.EmailAddress), DisplayName("Email address")> _
		Public Property Email() As String
			Get
				Return privateEmail
			End Get
			Set(ByVal value As String)
				privateEmail = value
			End Set
		End Property

		Private privatePassword As String
		<Required, ValidatePasswordLength, DataType(DataType.Password), DisplayName("Password")> _
		Public Property Password() As String
			Get
				Return privatePassword
			End Get
			Set(ByVal value As String)
				privatePassword = value
			End Set
		End Property

		Private privateConfirmPassword As String
		<Required, DataType(DataType.Password), DisplayName("Confirm password")> _
		Public Property ConfirmPassword() As String
			Get
				Return privateConfirmPassword
			End Get
			Set(ByVal value As String)
				privateConfirmPassword = value
			End Set
		End Property
	End Class
	#End Region

	#Region "Services"
	' The FormsAuthentication type is sealed and contains static members, so it is difficult to
	' unit test code that calls its members. The interface and helper class below demonstrate
	' how to create an abstract wrapper around such a type in order to make the AccountController
	' code unit testable.

	Public Interface IMembershipService
		ReadOnly Property MinPasswordLength() As Integer

		Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean
		Function CreateUser(ByVal userName As String, ByVal password As String, ByVal email As String) As MembershipCreateStatus
		Function ChangePassword(ByVal userName As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
	End Interface

	Public Class AccountMembershipService
		Implements IMembershipService
		Private ReadOnly _provider As MembershipProvider

		Public Sub New()
			Me.New(Nothing)
		End Sub

		Public Sub New(ByVal provider As MembershipProvider)
			_provider = If(provider, Membership.Provider)
		End Sub

		Public ReadOnly Property MinPasswordLength() As Integer Implements IMembershipService.MinPasswordLength
			Get
				Return _provider.MinRequiredPasswordLength
			End Get
		End Property

		Public Function ValidateUser(ByVal userName As String, ByVal password As String) As Boolean Implements IMembershipService.ValidateUser
			If String.IsNullOrEmpty(userName) Then
				Throw New ArgumentException("Value cannot be null or empty.", "userName")
			End If
			If String.IsNullOrEmpty(password) Then
				Throw New ArgumentException("Value cannot be null or empty.", "password")
			End If

			Return _provider.ValidateUser(userName, password)
		End Function

		Public Function CreateUser(ByVal userName As String, ByVal password As String, ByVal email As String) As MembershipCreateStatus Implements IMembershipService.CreateUser
			If String.IsNullOrEmpty(userName) Then
				Throw New ArgumentException("Value cannot be null or empty.", "userName")
			End If
			If String.IsNullOrEmpty(password) Then
				Throw New ArgumentException("Value cannot be null or empty.", "password")
			End If
			If String.IsNullOrEmpty(email) Then
				Throw New ArgumentException("Value cannot be null or empty.", "email")
			End If

			Dim status As MembershipCreateStatus
			_provider.CreateUser(userName, password, email, Nothing, Nothing, True, Nothing, status)
			Return status
		End Function

		Public Function ChangePassword(ByVal userName As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean Implements IMembershipService.ChangePassword
			If String.IsNullOrEmpty(userName) Then
				Throw New ArgumentException("Value cannot be null or empty.", "userName")
			End If
			If String.IsNullOrEmpty(oldPassword) Then
				Throw New ArgumentException("Value cannot be null or empty.", "oldPassword")
			End If
			If String.IsNullOrEmpty(newPassword) Then
				Throw New ArgumentException("Value cannot be null or empty.", "newPassword")
			End If

			' The underlying ChangePassword() will throw an exception rather
			' than return false in certain failure scenarios.
			Try
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
				Dim currentUser As MembershipUser = _provider.GetUser(userName, True)
				Return currentUser.ChangePassword(oldPassword, newPassword)
			Catch e1 As ArgumentException
				Return False
			Catch e2 As MembershipPasswordException
				Return False
			End Try
		End Function
	End Class

	Public Interface IFormsAuthenticationService
		Sub SignIn(ByVal userName As String, ByVal createPersistentCookie As Boolean)
		Sub SignOut()
	End Interface

	Public Class FormsAuthenticationService
		Implements IFormsAuthenticationService
		Public Sub SignIn(ByVal userName As String, ByVal createPersistentCookie As Boolean) Implements IFormsAuthenticationService.SignIn
			If String.IsNullOrEmpty(userName) Then
				Throw New ArgumentException("Value cannot be null or empty.", "userName")
			End If

			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie)
		End Sub

		Public Sub SignOut() Implements IFormsAuthenticationService.SignOut
			FormsAuthentication.SignOut()
		End Sub
	End Class
	#End Region

	#Region "Validation"
	Public NotInheritable Class AccountValidation
		Private Sub New()
		End Sub
		Public Shared Function ErrorCodeToString(ByVal createStatus As MembershipCreateStatus) As String
			' See http://go.microsoft.com/fwlink/?LinkID=177550 for
			' a full list of status codes.
			Select Case createStatus
				Case MembershipCreateStatus.DuplicateUserName
					Return "Username already exists. Please enter a different user name."

				Case MembershipCreateStatus.DuplicateEmail
					Return "A username for that e-mail address already exists. Please enter a different e-mail address."

				Case MembershipCreateStatus.InvalidPassword
					Return "The password provided is invalid. Please enter a valid password value."

				Case MembershipCreateStatus.InvalidEmail
					Return "The e-mail address provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidAnswer
					Return "The password retrieval answer provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidQuestion
					Return "The password retrieval question provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.InvalidUserName
					Return "The user name provided is invalid. Please check the value and try again."

				Case MembershipCreateStatus.ProviderError
					Return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."

				Case MembershipCreateStatus.UserRejected
					Return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."

				Case Else
					Return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator."
			End Select
		End Function
	End Class

	<AttributeUsage(AttributeTargets.Class, AllowMultiple := True, Inherited := True)> _
	Public NotInheritable Class PropertiesMustMatchAttribute
		Inherits ValidationAttribute
		Private Const _defaultErrorMessage As String = "'{0}' and '{1}' do not match."
		Private ReadOnly _typeId As Object = New Object()

		Public Sub New(ByVal originalProperty As String, ByVal confirmProperty As String)
			MyBase.New(_defaultErrorMessage)
			OriginalProperty = originalProperty
			ConfirmProperty = confirmProperty
		End Sub

		Private privateConfirmProperty As String
		Public Property ConfirmProperty() As String
			Get
				Return privateConfirmProperty
			End Get
			Private Set(ByVal value As String)
				privateConfirmProperty = value
			End Set
		End Property
		Private privateOriginalProperty As String
		Public Property OriginalProperty() As String
			Get
				Return privateOriginalProperty
			End Get
			Private Set(ByVal value As String)
				privateOriginalProperty = value
			End Set
		End Property

		Public Overrides ReadOnly Property TypeId() As Object
			Get
				Return _typeId
			End Get
		End Property

		Public Overrides Function FormatErrorMessage(ByVal name As String) As String
			Return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, OriginalProperty, ConfirmProperty)
		End Function

		Public Overrides Function IsValid(ByVal value As Object) As Boolean
			Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(value)
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
			Dim originalValue As Object = properties.Find(OriginalProperty, True).GetValue(value)
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
			Dim confirmValue As Object = properties.Find(ConfirmProperty, True).GetValue(value)
			Return Object.Equals(originalValue, confirmValue)
		End Function
	End Class

	<AttributeUsage(AttributeTargets.Field Or AttributeTargets.Property, AllowMultiple := False, Inherited := True)> _
	Public NotInheritable Class ValidatePasswordLengthAttribute
		Inherits ValidationAttribute
		Private Const _defaultErrorMessage As String = "'{0}' must be at least {1} characters long."
		Private ReadOnly _minCharacters As Integer = Membership.Provider.MinRequiredPasswordLength

		Public Sub New()
			MyBase.New(_defaultErrorMessage)
		End Sub

		Public Overrides Function FormatErrorMessage(ByVal name As String) As String
			Return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, _minCharacters)
		End Function

		Public Overrides Function IsValid(ByVal value As Object) As Boolean
			Dim valueAsString As String = TryCast(value, String)
			Return (valueAsString IsNot Nothing AndAlso valueAsString.Length >= _minCharacters)
		End Function
	End Class
	#End Region

End Namespace
