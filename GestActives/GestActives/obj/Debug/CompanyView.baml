   M S B A M L   `   `   `  ����F  BGestActives, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GestActivesGestActives  5   	       GestActives.CompanyView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:GestActives     5   	     � Title$  
Compañias��5   	   $	��550��6   $	��900��6(   
������� � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5	       � WindowStartupLocation$ CenterOwner=�65    J� CommandBindings 5
      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$, &local:CustomCommands.BuscarNameCompany��5      5        5   
   -   5        $) #local:CustomCommands.LimpiarCompany��5      5        5   
   -   5        $( "local:CustomCommands.GrabarCompany��5      5        5   
   -   5        $* $local:CustomCommands.EliminarCompany��5      5        5   
   -   5        $, &local:CustomCommands.ActualizarCompany��5      5       � 	Resources 5      ,   &GestActives.FieldsEmptyToBoolConverter   FieldsEmptyToBoolConverter �� 
TargetType  ExternaTextBoxStyle  Mediano �� BasedOn	 �� Triggers
 �� Binding �� ElementName �� Path �� Value �� Property �� Value��%g  &	        &	      7    5   
   6+   �� 5!   
   " ��6   8   	 5"      �� 5#      
 6   �� $ externaChecBox��$ 	IsChecked� True6]   .a�5$      ��  � ��$ Externa��6-   5%      �� 5&      
 6   �� $ externaChecBox��$ 	IsChecked�	 False6]   .a�5'      ��  � ��$ Interna��6-   5(      5*      5,      .��5.      � $��10q�6   
������� � ColumnDefinitions 50   
   �� 51      $��* �6   6+   �� 52      $	��250 �6   6-    � RowDefinitions 55   
   �� 56      $��50 �6   6*   �� 57      $��* �6   6)   �� 58      $��50 �6   6*   .+�5;   
   �� $��0��6   $��2��6"     
Encabezado#��[ 66   .+�5<      ��  
 Titulo#��[ 6   .��64   Gestión de compañias6J   5=      �� 5?   
   $��1��6   $��0��6"   
�������$��5q�6E   .+�5@      &   System.Windows.Controls.DataGrid  -   5          Name$ listCompanyDataGrid��5@         AutoGenerateColumns .     
IsReadOnly . ��64   �� *	Companies+   Columns 5B      0  *System.Windows.Controls.DataGridTextColumn  5C      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6S      CanUserResize .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	IdCompany+6^     5D      $ Nombre��6)   $	 100 6R    .   69   �� *Name+6^   4  .System.Windows.Controls.DataGridTemplateColumn  5E      $ Externa��6-   $ 50 6>    .     CellTemplate 5F      �� 5G      .R�5H   "   �� $��Center=�6J    d� 	IsChecked 6+   �� *
External+6J   5I      5K        5L      $ 	Teléfono��6)   $ 70 6Y    .   6;   �� *	Telephone+6d     5M      $ Email��6)   $ * 6R    .   68   �� *Email+6\   5O      5P      �� 5R   
   -   5         ��Name$ formularioStackPanel��5R      $��1��63   $��1��6@   
�������$��5q�6f   .+�5S      �� 
�������$��5q�60   .+�5T      �� .��6   Nombre Compañia6)   �� 5U      -   5         ��Name$ nombreTextBox��5U      #��[ 61   6T   �� 5V      -	   5         ��Name$ buscarButton��5V      $,��&local:CustomCommands.BuscarNameCompany��6/     Actions#��[ 6`   $��Buscar��5W      
 � Tag$ Imagenes/look.ico��6   $
��Left=�66   {�5X      `� 5Y        `� 	Converter#  [  6'   .1�5Z      �� $ externaChecBox��6&   $ 	IsChecked�6C   �� 5[      $ telefonoTextBox��6&   $
 Text�6D   �� 5\      $ emailTextBox��6&   $
 Text�6A   5]      5_      5`      �� 5a      
�������$��5q�60   .+�5b      �� .��6   Tipo de compañia6*   �� 5c      ! �� Orientation$! 
Horizontal=�6   .+�5d      �� -
   5        " ��Name$" externaChecBox��5d      $��Center=�67   6�   �� 5e      -   5        $ externaTextBox��5e      #��[ 66   # �� 
IsReadOnly# . 6w   5f      5g      �� 5h      
�������$��5q�60   .+�5i      �� .��6   	Teléfono6"   �� 5j      -   5        $ telefonoTextBox��5j       	 Corto#��[ 63   6u   5k      �� 5l      
�������$��5q�60   .+�5m      �� .��6   Email6   �� 5n      -   5        $ emailTextBox��5n       	 Largo#��[ 60   6r   5o      5p      �� 5s   
   $��2��6   $��0��6"   $! 
Horizontal=�62   
�������.+�5t      �� -   5        $ grabarButton��5t      #��[ 6+   $(��"local:CustomCommands.GrabarCompany��5u      {�.  $��Grabar��5v      $ Imagenes/save.ico��60   �� 5x      -   5        $ actualizarButton��5x      #��[ 6/   {�.  $��
Actualizar��5y      $ Imagenes/insert.ico��64   $,��&local:CustomCommands.ActualizarCompany��5z      �� 5|      -   5        $ eliminarButton��5|      #��[ 6-   $*��$local:CustomCommands.EliminarCompany��5}      {�.  $��Eliminar��5~      $ Imagenes/delete.ico��62   �� 5�      -   5        $ limpiarButton��5�      #��[ 6,   $)��#local:CustomCommands.LimpiarCompany��5�      $��Limpiar��5�      $ Imagenes/clear.ico��6   6:   5�      �� 5�   
   $��2��6   $��1��6"   
�������.+�5�      �� -   5        $ salirButton��5�      #��[ 6*   $��Right=�6K   $��Salir��5�      $ Imagenes/exit.ico��6%   6=   5�      5�      5�      