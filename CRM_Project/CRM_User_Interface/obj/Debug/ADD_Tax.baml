   M S B A M L   `   `   `  ����  CRM_User_Interface     CRM_User_Interface.ADD_Tax   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  ADD_Tax��5   	   $	��485��6   $	��719��6&    � WindowStartupLocation$ CenterScreen=�62   $��0,0,0,0q�6X    � WindowState$ Normal=�6i    � AllowsTransparency .  � WindowStyle$
 None=�6�    � ShowInTaskbar .  
������ .��5      �� 
������ $��1q�6)   $	��485��6=   $	��Top=�6J    �� CornerRadius$ 15��6b   
���   �$��	0,0,0,-63q�6�   ._�5   
   � ���	#FFEEEEF2$��14,29,13,14q�6&   .+�5      �� -   5         ��Name$ 
btnTaxMain��5      $
��Exit��6)   $
��Left=�68   $	��Top=�6S   $��50��6k   $��640,-25,0,0q�6v   6�   � 5      $
��Left=�6   $	��425��6.   $	��Top=�6;   $	��680��6S   $��5,10,0,0q�6_   
�������.+�5	      �� $
��Tax ��6   $
��Left=�6'   $	��Top=�6B   $��	311,2,0,0q�6Z   $��16(�6m   $
��Bold"�6{   �� 5
      $	��Tax��6   $
��Left=�6(   $	��Top=�6C   $��5,30,0,0q�6[   $	��671��6m   $��60��6y   .��5      � $
��Left=�6   $��45��66   $	��Top=�6B   $	��670��6Z   $��-5,-5,-6,-2q�6f   .+�5      �� $��Tax Type Name :��6    $
��Left=�6:   $	��Top=�6U   $��9,8,0,0q�6m   �� 5      -   5         ��Name$ txtTax_TName��5   "   $
��Left=�68   $��23��6S   	 �� TextWrapping$
	 Wrap=�6_   $	��Top=�6s   $	��160��6�   $��
109,10,0,0q�6�   �� 5      $��Tax Percentage :��6    $
��Left=�6;   $	��Top=�6V   $��	268,8,0,0q�6n   �� 5      -   5        $ txtTax_TPercent��5   "   $
��Left=�6;   $��23��6V   $
	 Wrap=�6b   $	��Top=�6v   $	��145��6�   $��
364,11,0,0q�6�   �� 5      -   5        $ btnTax_AddTax��5   !   $	��Tax��68   $
��Left=�6F   $	��Top=�6a   $	��110��6y   $��	549,9,0,0q�6�   $��25��6�   6�   �� 5      $��%��6    $
��Left=�6,   $	��Top=�6G   $��	509,9,0,0q�6_   5      5      &   System.Windows.Controls.DataGrid  5      -   5        
  Name$
 dgrd_Tax��5      $
��Left=�6-   $��	8,100,0,0q�6H      AutoGenerateColumns .  $	��Top=�6w   $	��245��6�   $	��667��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Sr No.��6-   -   5        
grsrno��   SortMemberPath$ ID��5   N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6b   �� *ID+ �� Mode$ TwoWay=�  5         Width5  /System.Windows.Controls.DataGridLengthConverter$
  300 6.   $ Tax Type��6;   -   5        
grdTaxType��$ Tax_Type��5   b    6|   �� *
Tax_Type+$ TwoWay=�  5      $
  200 6-   $ Tax Percentage��6:   -   5        grdPercentage��$ Tax_Percentage��5   j    6�   �� *Tax_Percentage+$ TwoWay=�4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 Edit��61   -	   5        Action2��   CellTemplate 5      �� 5   "   .R�5   &   �� $��btntaxoptions��6=   -
   5        $
��Edit��5   .   6R   5    #   5"        5#      $ Delete��61   -   5        
Action�� 5$      �� 5%   "   .R�5&   &   �� $��btntaxoptions��6S   -   5        $��Delete��5&   B   6S   5'   &   5)      5,      5-      �� 5.      $��Add Tax��6   $
��Left=�6&   
�������$	��Top=�6U   $��	9,-29,0,0q�6m   $��32��6�   50      51      53      