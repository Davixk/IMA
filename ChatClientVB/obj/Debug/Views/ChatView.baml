   M S B A M L   `   `   `  ����N  JInstantMessagingApp, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ChatClientVBChatClientVB  5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null{Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters 5	          ChatClientVB.ChatView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      &localclr-namespace:ChatClientVB  5      X TMaterialDesignThemes.MahApps, Version=0.0.11.0, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      f
ConvertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf   5	         MaterialDesignPaper#���   5
        � 	Resources  5        SendButtonStyle �� 
TargetType �� Property �� Value  PrimaryHueDarkBrush �� 
TargetType'  !MaterialDesignThemes.Wpf.PackIcon�  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind J� SnapsToDevicePixels �� Triggers P� Property	 P� Value
 �� 
TargetName  PrimaryHueMidBrush  ImageButtonStyle �� BasedOn   ParticipantsListBoxItemStyle �� RelativeSource �� AncestorType ��Name �� Padding ]� 
Conditions �� Property �� Value  ParticipantsDataTemplate  MaterialDesignDivider �� Path � ColumnDefinitions ��Name J� OpacityMask !� Visual �� ElementName A�Name  ByteBmpSrcConverter �� 	Converter 	 
BlankImage �� TargetNullValue J� Opacity �� Triggers ^� 
Conditions �� Binding  �� Triggers! �� Value" �� Binding 
 MessagesDataTemplate# �� CornerRadius$ � RowDefinitions% �Name& � Cursor' � ToolTip.  (System.Windows.Interactivity.Interaction(   Triggers/  )System.Windows.Interactivity.EventTrigger)   	EventName.  (System.Windows.Interactivity.TriggerBase*   Actions6  0System.Windows.Interactivity.InvokeCommandAction+   Command,   CommandParameter- ��Name. �� StringFormat��%l  &	       &	 �    7  &	 x    &	 �
    7  7 	 &	
 �    �� 5   
   " ��6(   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � ��# �  6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ Medium"�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��$ Stretch=�6;   �� 5       � ��$ Stretch=�69   �� 5       � ��$ 0q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � ���	#00000000.+�5        $��SendIcon��66   $
 Send=�6F    . #��z��5   6   #��z��5   6   #��z��5    6   #��z��5!   6   $
��Auto��6Y   $
��Auto��6f   5"       5#      P� 5$       �@J�IsMouseOver	 . . �5%   "   �� $
 SendIcon��6?   # �  6U    � ��5&      P� 5'       �@J�IsKeyboardFocused	 . . �5(   "   �� $
 SendIcon��6?   # �  6U    � ��5)      P� 5*       � {�	 .  . �5+   "   ��  �@J�Opacity$
 0.23Q�6<   5,      5.      50      51      �� 53   
   " ��6)   8   .�54      ��  � �� 55      �� 56      " ��6&   .R�57      � ���	#00000000.+�58        $��	PhotoIcon��66   $ Image=�6G    . #��z��59   6   #��z��5:   6   #��z��5;   6   #��z��5<   6   $
��Auto��6Y   $
��Auto��6f   5=       5>      P� 5?       �@J�IsMouseOver	 . . �5@   "   �� $
 	PhotoIcon��6?   # �  6V    � ��5A      P� 5B       �@J�IsKeyboardFocused	 . . �5C   "   �� $
 	PhotoIcon��6?   # �  6V    � ��5D      P� 5E       � {�	 .  . �5F   "   ��  �@J�Opacity$
 0.23Q�6<   5G      5I      5K      5L      �� 5N   
   " ��65   .�5O      ��  � ��
 ���� 6+   �� 5P       � �� 5Q      �� *HorizontalContentAlignment+ �� " ���� 5R       � �� 5S      �� *VerticalContentAlignment+ �� " ���� 5T       � ��$ 2,0,0,0q�6(   �� 5U       � ��$ 0q�6'   �� 5V       � �� 5W      �� 5X      " ��6&   .R�5Y      �� $ Bd��6!   #��z��5Z   !   #��z��5[   !   #��z��5\   !   # z��5]   !    . ._�5^      �� $��1��6/   #��z��5_   /   # z 5`   /   #��z��5a   /   5b       5c      P� 5d       �@��
IsSelected	 . . �5e   "   �� $
 Bd��6?    � �� �#E3F2FD6O   �� 5f   "   # � .�5g   )    � ��5h      ]� 5i       5j   "   �� 5k   &    �@��
IsSelected . 6F   �� 5l   &    �@��IsSelectionActive .  6V   ./�5n   "   �� $
 Bd��6?    � �� �#E3F2FD6O   �� 5o   "   # � .�5p   )    � ��5q      P� 5r       � {�	 .  . �5s   "   �� # � ��6?    � ��5t      5v      5x      5y      �� 5{   
   .R�5|      �� $��0,0,0,1q�6   #���  6/   $��50��5~      $��-2,0,0,0q�6!   ��5}      �� $ ActualWidth� �� *FindAncestor+$ ListBoxItemL�._�5      �  5�      �� 5�      $��5 �6+   �� 5�      $��50 �6+   �� 5�      �� 5�      $��20 �6+   �� 5�      $��20 �6+   .+�5�      �� $ ParticipantRct��6    #���  68   $y�Hidden=�5�       � 5�      $��1��6   $��6q�6+    .  5�      !� 5�       6*   �� $ ClipEllipse��.+�5�      A� $ ClipEllipse��6"   
�������67     5�      $ AccountCircle=�62    . $
��Auto��6b   $
��Auto��6o   $��-4q�5�   2   $��Stretch=�6>   $��Stretch=�6\   �� 5�      $��UniformToFill=�5�   K   ��5�       �� *
imgProfilo+8   8  5�   K   5�      �� 5�      $��2��6    $��Center=�60   $
��Left=�6K   $	��5,0q�5�       $��SemiBold"�6-   $��CharacterEllipsis=�6C    . ��5�       �� *Nome+66     5�      $��NewMessageIcon��6.   $��3��6D    . $��Center=�5�   .   $��Center=�6I   $ MessageReplyText=�5�   .   $	 0.7Q�6F   $y�Hidden=�6T     5�      $��
TypingIcon��6.   $��3��6@    . $��Center=�5�   .   $��Center=�6I   $ Feather=�5�   .   $	 0.8Q�6=   $y�Hidden=�6K   A� 5�      $��4��6   $��Center=�6.   $��Center=�6I   $��8��5�      $��8��6(   ��5�      �� 5�      " A�6$   .�5�   "   ��  � �� �#F4433669    5�   "   ^� 5�   &    5�   *   �� 5�   .    68   �� *DataContext.Connesso+ �� *FindAncestor+$ UserControlL� True5�   z   �� 5�   .    68   �� *	Loggato+ True6T   0�5�   *   �� 5�   .    � �� �#64DD176E   5�   '   5�      5�      5�      5�        5�      �� 5�      $
! True��6J   " 6   �� *HaInviatoNuovoMessaggio+.a�5�      �� $
 NewMessageIcon��6    � y�$ Visible=�6O   5�      �� 5�      $
! True��6?   " 6   �� *StaScrivendo+.a�5�      �� $
 NewMessageIcon��6    � y�$ Hidden=�6O   �� 5�      $
 
TypingIcon��6    � y�$ Visible=�6K   5�      �� 5�      $
! true��5�   ^   " 5�      �� $ 
IsSelected� �� *FindAncestor+$ ListBoxItemL�.a�5�      �� $
 ParticipantRct��63    � y�$ Visible=�6O   5�      5�      �� 5�   
   .R�5�      �� $��MessageBorder��6   $��40��6*   $	��280��69   $��1q�6H   ���#EFEBE9$��
10,0,60,10q�5�      ���#BCAAA4$# 4��6?    . $
��Left=�5�      ._�5�      � $ 5�      �� 5�      �� 5�      �� 5�      $��15 �6(   .+�5�      � $% ImageMessageGrid��6   $��6,6,6,5q�65    5�      !� 5�       6*   �� $ 
ClipBorder��.+�5�      �� $ 
ClipBorder��6!   $# 3��65   
�������6F   �� 5�      $��UniformToFill=�6    $
& Hand��68   $;' 5Clicca per aprire immagine nel tuo viewer predefinito��5�       ��5�       �� *
Immagine+( 5�        5�   "   $) MouseLeftButtonUp��61   .* 5�   &     + 5�   )   �� *DataContext.OpApriImmagine+ �� *FindAncestor+$ UserControlL�, 5�   )   �� 5�   #   5�      5�      �� 5�      $- MessageTxtBlock��6    $��1��69   $��7,5,7,0q�6F   $
��Wrap=�6W   $��Stretch=�5�       $��Stretch=�6<   ��5�       �� *Testo+�� 5�      $��2��6    $��Right=�6-   $��Stretch=�6I   $��0,0,5,0q�5�       $��10(�61   $	 0.8Q�6?   ��5�       �� *	DataOra+$. {0:t}��5�      5�        5�      �� 5�      $
! True��6A   " 6   �� *IsOriginNative+.a�5�      �� $
 MessageBorder��6    � ��$ Right=�6W   �� 5�      $
 MessageBorder��6    � ��$ 
60,0,10,10q�6J   �� 5�      $
 MessageBorder��6    � �� �#BBDEFB6N   �� 5�      $
 MessageBorder��6    � �� �#64B5F66O   5�      �� 5�      " 6   �� *
Immagine+! 6;   Z� .a�5�      �� $
 ImageMessageGrid��6    � y�$ 	Collapsed=�6Q   5�      �� 5�      " 6   �� *Testo+! 68   Z� .a�5�      �� $
 MessageTxtBlock��6    � y�$ 	Collapsed=�6P   �� 5�      $
 MessageBorder��6    � ��$	 320��6L   5�      ^� 5�       5�      �� 5�       6$   �� *Testo+ 6>   Z� �� 5�       6$   �� *IsOriginNative+ True6G   0�5      �� 5     $
 MessageBorder��6!    � ��$ 	0,0,10,10q�6N   �� 5     $
 MessageBorder��6!    � ��$ Right=�6[   5     5     5     .��5	     � $ 5
  
   �� 5     �� 5     $
��Auto �6    5  
   �� 5     $	��220 �6   �� 5     .+�5  
   �� $��2��6   $��0,0,1,0q�6"    . #���  5     ._�5     �� #���  5     #���  5     $��Disabled=�5     $
��Auto=�5     ��5     �� *Partecipanti+/ �� SelectedItem/ 5     �� *PartecipanteSelezionato+5     5     �� 5  
   -   5        0 ��Name$0 MessagesItemsCtrl��5     $��1��62   $��0,5,0,0q�6B   #��� 
 5      $
��Auto=�5!     ��5     �� *!PartecipanteSelezionato.Chatter+1   	Behaviors1 5"     /   )ChatClientVB.BringNewItemIntoViewBehavior  5#     ��5%     �� 5&     " ��6"   .R�5'     �� .��5(     �� 5)     5*     5,     �� 5.  
   $��1��6   $��1��6    . #���  5/     ._�50     �  51     �� 52     �� 53     $��50 �6'   .+�56     �� $��
10,10,0,10q�6   $��80��6-   $��1q�57     #���  6-   ._�58     �  59     �� 5:     �� 5;     $��30 �6/   .+�5>     �� -   5        2 ��Name$2 MessageTxtBox��5>  "   
������ 3 �� TextWrapping$
3 Wrap=�5?  "   4 �� AcceptsReturn4 . 5 �� 
CaretBrush5 �	#7F000000"6 �� VerticalScrollBarVisibility$
6 Auto=�5@  "   ��5A  "   �� *
MsgTesto+7 �� UpdateSourceTrigger$7 PropertyChanged=�.��6a   ( 5B       5C  "   $) KeyUp��61   .* 5D  &     + 6<   �� *OpStaScrivendo+5E  #   .��5F  6   5G     �� 5I     $��1��6!   ���	#FF6E6E6E$��4q�6H   #���  5J  !   ��5K  !   �� *OpInviaMsgImmagine+5L     5M     �� 5O     -   5        8 ��Name$8 
SendButton��5O     $��1��6-   $��10q�6=   #���  5P     ��5Q     �� *OpInviaMsgTesto+5R     5S     5T     5U     