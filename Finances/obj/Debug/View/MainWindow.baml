   M S B A M L   `   `   `  ����C  ?Finances, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Finances.ViewFinances.View  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	       Finances.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Finances.View     5   	   _iePclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   $	��700��5	   	   $
��1000��6     � WindowStartupLocation$  CenterScreen=�6#    � WindowStyle$ SingleBorderWindow=�6I   $��Kornilow)�6j    � 	Resources 5
         MyCursor�� 5   
   %d   &	        ��5      FBC:\Users\Джек\Downloads\battlefield 3 Purple Revival\arrow.cur��6f   5      .��5      �  � Cursor# [  6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6   $��3��6,   ���#e1bee76@   �� 5   
   -   5         ��Name$
 list��5   m   $��0��6   $��1��6   
������ $��20(�6H   ��6V   Z� ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   $��Stretch=�6:   $��Stretch=�6X   5      ��5!      �� 5"       �� 
TargetType" ��6   .�5#      �� 	 �� Property	 � ��
 �� Value
 5$      �� 5%       �� 
TargetType" ��6.   .R�5&   "   �� $��Bd��5*      #��z��5&   )    �� Padding# z��5'      #��z��5(      #��z��5)       J� SnapsToDevicePixels . ._�5,   &   �� #��z��67   #��z��5-        �� ContentStringFormat �� ContentStringFormat# z 5.       #��z��5/       #��z��50       # z 51       52   #    �� Triggers 53   "   ]� 54   &    ]� 
Conditions 55   *   �� 56   .    �� Property �@��IsSelectionActive �� Value .  6^   �� 57   .    �@��
IsSelected . 6W   ./�59   *   ��  �� 
TargetName$ Bd��6M   	 � ��
 �#d5b4db5:      �� 5;   *   $ Bd��6O   	 � ��
 �#d5b4db6_   5<   '   ]� 5=   &    5>   *   �� 5?   .    �@��IsSelectionActive . 6^   �� 5@   .    �@��
IsSelected . 6W   ./�5B   *   �� $ Bd��6M   	 � ��
 �#d5b4db6]   �� 5C   *   $ Bd��6O   	 � ��
 5D   .   ��5E   2   #d5b4db��6K   5G   +   5H   '   P� 5I   &    P� Property �@J�IsMouseOver P� Value . . �5J   *   �� $ Bd��6M   	 � ��
 �#dcbae25K      �� 5L   *   $ Bd��6O   	 � ��
 5M   .   ��5N   2   #dcbae2��6K   5P   +   5Q   '   P� 5R   &    � {� .  . �5S   *   �� $ Bd��6S   	 � ��
 5T   .   C� 5U   2    C� ResourceKey# Z��6B   5W   +   5X   '   5Z      5\      5]      .  (System.Windows.Interactivity.Interaction   Triggers 5_      /  )System.Windows.Interactivity.EventTrigger  5`         	EventName$ SelectionChanged��6"   .  (System.Windows.Interactivity.TriggerBase   Actions. 5a      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6-   �� *SelectCommand+   CommandParameter 6P   ��  �� ElementName$
 list�� �� Path$ SelectedItem�5b      .@�5d      �� -   5         ��Name$
 main��5d   (   $
 Hand��6   .��5e      �� $��Главная��6   5f      �� 5g      -   5        $ income��5g   (   $
 Hand��6   .��5h      �� $��Доходы��6   5i      �� 5j      -   5        $
 cost��5j   (   $
 Hand��6   .��5k      �� $��Расходы��6   5l      5m      �� 5n   
   $ 
Horizontal=�6   $��0��6.   $��0��6;   $
��Left=�6K   $��Center=�6f   .+�5o      �� $��50��6   $��50��6    $
 Hand��6,   $��0q�6:   $��0��6N     �� CornerRadius$  40��6[   $��10q�6m   ._�5p      A� ��5q      �� 5r      ! j� Stretch$! UniformToFill=�6%   " �� ImageSource" 6>   �� $ CurrentUser.Image�# �� UpdateSourceTrigger$# PropertyChanged=�6�   5t      $ J� InputBindings$ 5u      d� 5v      % d� MouseAction$% 	LeftClicke�6F   & �� Command& 6#   �� *ProfileCommand+6F   5x      �� 5y      $��18(�6   $��10q�6&   ���#616161$��Center=�6�   $��Center=�6�   ��6G   �� $ CurrentUser.Login�$# PropertyChanged=�6�   5z      �� 5{   
   $��2��6   $��0��6      ButImage#��[ 6-   $��10q�6O   $��Center=�6[   $��Center=�6v   $��Images\logout.png��6�   $��30��6�   $��30��6�   $ 5|      d� 5}      $% 	LeftClicke�5~   '   & 5}      �� *LogOutCommand+5~   '   5�      �� 5�   
   $��0��6   $��3��6+   $��1��6?   ��6L   �� $ 	CurrentVM�5�      5�      