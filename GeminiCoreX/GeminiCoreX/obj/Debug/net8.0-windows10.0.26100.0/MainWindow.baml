   M S B A M L   `   `   `  ����F  BGeminiCoreX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GeminiCoreXGeminiCoreX  5   	       GeminiCoreX.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:GeminiCoreX     5   	   \ XiNKORE.UI.WPF.Modern, Version=0.9.30.0, Culture=neutral, PublicKeyToken=cd19e634b9706635e aiNKORE.UI.WPF.Modern.Controls, Version=0.9.30.0, Culture=neutral, PublicKeyToken=cd19e634b97066356ui+http://schemas.inkore.net/lib/ui/wpf/modern   5   	   8  2iNKORE.UI.WPF.Modern.Controls.Helpers.WindowHelper    UseModernWindowStyle$
  True��5	   	      SystemBackdropType$
 Mica=�5
   	    � Title$  ��5   	   $	��450��6   $	��800��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��220 �6   �� 5      $��* �6   .+�5   
   �� .@�5      �� -   5         ��Name$
 LVI1��5   M   $��2,2,2,0q�6   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      ,  &iNKORE.UI.WPF.Modern.Controls.FontIcon  ;  5iNKORE.UI.WPF.Modern.Common.IconKeys.SegoeFluentIcons   Home   Icon# Z 6"   �� 5      $��首页��6    $��10,0,0,0q�6*   5      5      �� 5      -   5        $
 LVI2��5   O   $��2,0,2,0q�6   .��5      �� $ 
Horizontal=�6   .+�5           Error# Z 6"   �� 5      $��关于设备��6    $��10,0,0,0q�6,   5      5      �� 5      -   5        $
 LVI3��5   O   $��2,0,2,0q�6   .��5       �� $ 
Horizontal=�6   .+�5!        	   System# Z	 6"   �� 5"      $��系统功能��6    $��10,0,0,0q�6,   5#      5$      �� 5&      -   5        $
 LVI4��5&   O   $��2,0,2,0q�6   .��5'      �� $ 
Horizontal=�6   .+�5(        
   Pencil# Z
 6"   �� 5)      $��主题定制��6    $��10,0,0,0q�6,   5*      5+      �� 5-      -   5        $
 LVI5��5-   @   $��2,0,2,0q�6   $y�Hidden=�6,   .��5.      �� $ 
Horizontal=�6   .+�5/           More# Z 6"   �� 50      $��	占位符��6    $��10,0,0,0q�6+   51      52      53      �� 54   
   $��1��6   $
��Auto=�6'   $
��Auto=�6L   .��55       � -   5          �Name$ Frame1��55        � NavigationUIVisibility$ Hidden=�6$   56      57      58      