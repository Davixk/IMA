   M S B A M L   `   `   `  ���� N  JInstantMessagingApp, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ChatClientCS.ViewsChatClientCS.Views  5clr-namespace:ChatClientCS.DataChatClientCS.Data  7 clr-namespace:ChatClientCS.UtilsChatClientCS.Utils  V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null{Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters !    ChatClientCS.Views.ChatView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ,local clr-namespace:ChatClientCS.Views  *dataclr-namespace:ChatClientCS.Data  ,utils clr-namespace:ChatClientCS.Utils  X TMaterialDesignThemes.MahApps, Version=0.0.11.0, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   f
ConvertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf      MaterialDesignPaper#���     � 	Resources    SendButtonStyle �� 
TargetType �� Property �� Value  PrimaryHueDarkBrush �� 
TargetType'  !MaterialDesignThemes.Wpf.PackIcon�  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind J� SnapsToDevicePixels �� Triggers P� Property	 P� Value
 �� 
TargetName  PrimaryHueMidBrush  ImageButtonStyle �� BasedOn   ParticipantsListBoxItemStyle �� RelativeSource �� AncestorType ��Name �� Padding ]� 
Conditions �� Property �� Value  ParticipantsDataTemplate  MaterialDesignDivider �� Path � ColumnDefinitions ��Name J� OpacityMask !� Visual �� ElementName A�Name  ByteBmpSrcConverter �� 	Converter 	 
BlankImage �� TargetNullValue J� Opacity �� Triggers ^� 
Conditions �� Binding  �� Triggers! �� Value" �� Binding 
 MessagesDataTemplate# �� CornerRadius$ � RowDefinitions% �Name& � Cursor' � ToolTip.  (System.Windows.Interactivity.Interaction(   Triggers/  )System.Windows.Interactivity.EventTrigger)   	EventName.  (System.Windows.Interactivity.TriggerBase*   Actions6  0System.Windows.Interactivity.InvokeCommandAction+   Command,   CommandParameter- ��Name. �� StringFormat��%�  &	       &	 \    7  &	 �    &	 /    7  7 	 &	
 �    �� " ��.���  � ��
 ���� ��  � ��
 ���� ��  � ��# �  ��  �@�Cursor$
 Hand����  � ��$ Medium"���  � ��$ 14(���  � ��$ 1q���  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ 0q���  � �� �� " ��.R�� ���	#00000000.+�  $��SendIcon��$
 Send=� . #��z��#��z��#��z��#��z��$
��Auto��$
��Auto�� P�  �@J�IsMouseOver	 . . ��� $
 SendIcon��# �   � ��P�  �@J�IsKeyboardFocused	 . . ��� $
 SendIcon��# �   � ��P�  � {�	 .  . ���  �@J�Opacity$
 0.23Q��� " ��8   .���  � �� �� " ��.R�� ���	#00000000.+�  $��	PhotoIcon��$ Image=� . #��z��#��z��#��z��#��z��$
��Auto��$
��Auto�� P�  �@J�IsMouseOver	 . . ��� $
 	PhotoIcon��# �   � ��P�  �@J�IsKeyboardFocused	 . . ��� $
 	PhotoIcon��# �   � ��P�  � {�	 .  . ���  �@J�Opacity$
 0.23Q��� " ��.���  � ��
 ���� ��  � �� �� *HorizontalContentAlignment+ �� " ����  � �� �� *VerticalContentAlignment+ �� " ����  � ��$ 2,0,0,0q���  � ��$ 0q���  � �� �� " ��.R��� $ Bd��#��z��#��z��#��z��# z�� . ._��� $��1��#��z��# z #��z�� P�  �@��
IsSelected	 . . ��� $
 Bd�� � �� �#E3F2FD�� # � .� � ��]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive .  ./��� $
 Bd�� � �� �#E3F2FD�� # � .� � ��P�  � {�	 .  . ��� # � �� � ���� .R��� $��0,0,0,1q�#���  $��50��$��-2,0,0,0q����� $ ActualWidth� �� *FindAncestor+$ ListBoxItemL�._��  �� $��5 ��� $��50 ��� �� $��20 ��� $��20 �.+��� $ ParticipantRct��#���  $y�Hidden=�� $��1��$��6q� .  !�  �� $ ClipEllipse��.+�A� $ ClipEllipse��
�������  $ AccountCircle=� . $
��Auto��$
��Auto��$��-4q�$��Stretch=�$��Stretch=��� $��UniformToFill=����� *Photo+8   8  �� $��2��$��Center=�$
��Left=�$	��5,0q�$��SemiBold"�$��CharacterEllipsis=� . ���� *Name+  $��NewMessageIcon��$��3�� . $��Center=�$��Center=�$ MessageReplyText=�$	 0.7Q�$y�Hidden=�  $��
TypingIcon��$��3�� . $��Center=�$��Center=�$ Feather=�$	 0.8Q�$y�Hidden=�A� $��4��$��Center=�$��Center=�$��8��$��8������ " A�.���  � �� �#F44336 ^�  ��  �� *DataContext.IsConnected+ �� *FindAncestor+$ UserControlL� True��  �� *
IsLoggedIn+ True0���  � �� �#64DD17  �� $
! True��" �� *HasSentNewMessage+.a��� $
 NewMessageIcon�� � y�$ Visible=��� $
! True��" �� *
IsTyping+.a��� $
 NewMessageIcon�� � y�$ Hidden=��� $
 
TypingIcon�� � y�$ Visible=��� $
! true��" �� $ 
IsSelected� �� *FindAncestor+$ ListBoxItemL�.a��� $
 ParticipantRct�� � y�$ Visible=��� .R��� $��MessageBorder��$��40��$	��280��$��1q����#EFEBE9$��
10,0,60,10q����#BCAAA4$# 4�� . $
��Left=�._�� $ �� �� �� $��15 �.+�� $% ImageMessageGrid��$��6,6,6,5q� !�  �� $ 
ClipBorder��.+��� $ 
ClipBorder��$# 3��
��������� $��UniformToFill=�$
& Hand��$6' 0Click to open image in your default image viewer������ *	Picture+(   $) MouseLeftButtonUp��.*   + �� *DataContext.OpenImageCommand+ �� *FindAncestor+$ UserControlL�, �� �� $- MessageTxtBlock��$��1��$��7,5,7,0q�$
��Wrap=�$��Stretch=�$��Stretch=����� *	Message+�� $��2��$��Right=�$��Stretch=�$��0,0,5,0q�$��10(�$	 0.8Q����� *Time+$. {0:t}��  �� $
! True��" �� *IsOriginNative+.a��� $
 MessageBorder�� � ��$ Right=��� $
 MessageBorder�� � ��$ 
60,0,10,10q��� $
 MessageBorder�� � �� �#BBDEFB�� $
 MessageBorder�� � �� �#64B5F6�� " �� *	Picture+! Z� .a��� $
 ImageMessageGrid�� � y�$ 	Collapsed=��� " �� *	Message+! Z� .a��� $
 MessageTxtBlock�� � y�$ 	Collapsed=��� $
 MessageBorder�� � ��$	 320��^�  ��  �� *	Message+ Z� ��  �� *IsOriginNative+ True0��� $
 MessageBorder�� � ��$ 	0,0,10,10q��� $
 MessageBorder�� � ��$ Right=�.��� $ �� �� $
��Auto � �� $	��220 ��� .+��� $��2��$��0,0,1,0q� . #���  ._��� #���  #���  $��Disabled=�$
��Auto=����� *Participants+/ �� SelectedItem/ �� *SelectedParticipant+�� -   0 ��Name$0 MessagesItemsCtrl��$��1��$��0,5,0,0q�#��� 
 $
��Auto=����� *SelectedParticipant.Chatter+1   	Behaviors1 5   /ChatClientCS.Utils.BringNewItemIntoViewBehavior  ���� " ��.R��� .���� �� $��1��$��1�� . #���  ._��  �� �� $��50 �.+��� $��
10,10,0,10q�$��80��$��1q�#���  ._��  �� �� $��30 �.+��� -   2 ��Name$2 MessageTxtBox��
������ 3 �� TextWrapping$
3 Wrap=�4 �� AcceptsReturn4 . 5 �� 
CaretBrush5 �	#7F000000"6 �� VerticalScrollBarVisibility$
6 Auto=����� *TextMessage+7 �� UpdateSourceTrigger$7 PropertyChanged=�.��(   $) KeyUp��.*   + �� *TypingCommand+.���� $��1�����	#FF6E6E6E$��4q�#���  ���� *SendImageMessageCommand+�� -   8 ��Name$8 
SendButton��$��1��$��10q�#���  ���� *SendTextMessageCommand+