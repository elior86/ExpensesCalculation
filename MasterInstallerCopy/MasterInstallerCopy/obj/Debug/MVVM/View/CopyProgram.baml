   M S B A M L   `   `   `  ����N  JMasterInstallerCopy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:MasterInstallerCopy.MVVM.ViewModel"MasterInstallerCopy.MVVM.ViewModel  5   	   O,clr-namespace:MasterInstallerCopy.ConvertersMasterInstallerCopy.Converters  5   	   M+clr-namespace:MasterInstallerCopy.BehaviorsMasterInstallerCopy.Behaviors  5   	   %    MasterInstallerCopy.CopyProgram   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   F	viewModel0clr-namespace:MasterInstallerCopy.MVVM.ViewModel     5   	   C
converters,clr-namespace:MasterInstallerCopy.Converters     5   	   A	behaviors+clr-namespace:MasterInstallerCopy.Behaviors     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Minteractions:http://schemas.microsoft.com/expression/2010/interactivity  5   	   -   5        $��copyProgramWindow��5	   	     � Title$  CopyProgram��5   	   $	��350��6   $	��525��6*    � WindowStyle$
 None=�5
   	   
������  � AllowsTransparency .  � DataContext 5      =  7MasterInstallerCopy.MVVM.ViewModel.CopyProgramViewModel  5   
    � 	Resources 5       �� MergedDictionaries �� Source:   4MasterInstallerCopy.Converters.ValuestoListConverter   valuesToListConverter<   6MasterInstallerCopy.Converters.ValuesToButtonConverter  valuesToButtonConverter>   8MasterInstallerCopy.Converters.BoolToVisibilityConverter  boolToVisibilityConverter�� 5   
    5      �� 5      $D >/MasterInstallerCopy;component/Resources/ProgressBarStyle.xaml?�6%   �� 5      $I C/MasterInstallerCopy;component/Resources/WatermarkTextboxStyle.xaml?�6%   %W   &	        &	      &	 &       5      6/     5      61     5      63   5      .��5      ��  �� CornerRadius$ 10,10,10,10��6    �� Padding$ 6q�6(   
���   �$��1q�6H   ��5   
   �� 5       	 �� 
StartPoint$		 0,0&�6"   
 �� EndPoint$	
 0,3&�63   .��5!      �  � Color$ #3BB2EA��6    � Offset$ 0Q�6/   � 5"      $ #EFF7FA��6   $	 0.3Q�6/   5#      .  (System.Windows.Interactivity.Interaction   	Behaviors 5%   
   0   *MasterInstallerCopy.Behaviors.DragBehavior  5&      ._�5)   
   � -   5        $��CopyProgramGrid��5)       � RowDefinitions 5*      �� 5+      $
��Auto �6    �� 5,      $
��Auto �6    �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      $
��Auto �6     � ColumnDefinitions 51      �� 52      $
��Auto �6#   �� 53      $��* �6#   �� 54      $��* �6#   �� 55      $
��Auto �6#   .+�57      �� $��0��6   $��4��58      $��X��59      � 5;      $��1��6   $��4��6     5<      �� 5=      $
��Auto �6$   �� 5>      $
��Auto �6$    5@      �� 5A      $
��Auto �6'   �� 5B      $��* �6'   .+�5D      �� $��Source Path: ��6   $��0��5E      $��0��5F      �� 5G      $��Destination Path: ��6   $��1��5H      $��0��5I      �� 5J      -   5        $��sourceTxtBx��5J       �� TextWrapping$
 Wrap=�5K      
 � Tag$@ :Copy source path (example: ...\12.0\120.0.1021\Components)��5L      $��0��5M      $��1��5N       � ToolTip$- 'example: ...\12.0\120.0.1021\Components��5O       ^� InitialShowDelay$ 0��5P        WatermarkedTextBox#��[ 5Q      ��5R      ��  �� Path$ SourcePathText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5S      -   5        $��destinationTxtBx��5S      $
 Wrap=�5T      $9 3Copy destination path (example: ...\MOI\Components)��5U      $��1��5V      $��1��5W      $! example: ...\MOI\Components��5X      $ 0��5Y      #��[ 5Z      ��5[      �� $ DestinationPathText�$ TwoWay=�$ PropertyChanged=�5\      �� 5^      $��2��6   $��0��6&   .+�5_      � -   5        $��WhatToCopyGrid��5_       5`      �� 5a      $
��Auto �6(   �� 5b      $
��Auto �6(   �� 5c      $
��Auto �6(   �� 5d      $
��Auto �6(   �� 5e      $
��Auto �6(   .+�5g      �� $��Choose what to copy��6   $��0��6:   �� 5h      -   5        $��mccChkBx��5h      $	��MCC��5i      $��1��5j      $	��MCC��5l      ��5k      �� *CheckBoxCommand+5l      �� 5m      -   5        $��
mccLpChkBx��5m      $��MCC Language Pack��61   $��2��6M   �� 5n      -   5        $��sysAdminChkBx��5n      $��SysAdmin��64   $��3��6G   �� 5o      -	   5        $��sysAdminLpChkBx��5o      $��SysAdmin Language Pack��66   $��4��6W   5p      5q      �� 5r      -
   5        $��copyBtn��5r      $��
Copy Files��5s      $��2��5t      $��1��5u      $��Center=�5v      $��Center=�5w      ��5x      �� *ButtonCommand+��5y      `� 5z       `� 	Converter# [  6#   .1�5{      ��  �� ElementName$ sourceTxtBx��6"   $
 Text�6<   �� 5|      $ destinationTxtBx��6"   $
 Text�6A   �� 5}      $ mccChkBx��6"   $ Content�69   �� 5~      $ mccChkBx��6"   $ 	IsChecked�69   �� 5      $ 
mccLpChkBx��6"   $ Content�6;   �� 5�      $ 
mccLpChkBx��6"   $ 	IsChecked�6;   �� 5�      $ sysAdminChkBx��6"   $ Content�6>   �� 5�      $ sysAdminChkBx��6"   $ 	IsChecked�6>   �� 5�      $ sysAdminLpChkBx��6"   $ Content�6@   �� 5�      $ sysAdminLpChkBx��6"   $ 	IsChecked�6@   5�      {�5�      `� 5�      # [ 6#   .1�5�      �� $ sourceTxtBx��6"   $
 Text�6<   �� 5�      $ destinationTxtBx��6"   $
 Text�6A   �� 5�      $ mccChkBx��6"   $ 	IsChecked�69   �� 5�      $ 
mccLpChkBx��6"   $ 	IsChecked�6;   �� 5�      $ sysAdminChkBx��6"   $ 	IsChecked�6>   �� 5�      $ sysAdminLpChkBx��6"   $ 	IsChecked�6@   5�      5�      9� 5�      -   5        $��expenderBtn��5�      $��2��5�      $��3��5�      $��Center=�5�       9� ExpandDirection$ Right=�5�      � 5�      -   5        $��expenderPopup��5�      $��Right=�5�       � AllowsTransparency . $��Scroll=�5�      ��5�      �� $ expenderBtn��$ 
IsExpanded�.��5�      �� 
���   �$��1q�6-   $ 10,10,10,10��6A   $ 6q�6\   ��5�      �� 5�      $		 0,0&�6.   $	
 0,3&�6?   .��5�      � $ #3BB2EA��6+   $ 0Q�6;   � 5�      $ #EFF7FA��6+   $	 0.3Q�6;   5�      ._�5�      ��  �� Orientation$ 
Horizontal=�6!   .+�5�      �� -   5        $��popupTxtBlock��5�   $   $	��300��5�   $   $	��180��5�   $   $��10,10,10,10q�5�   $   $
��Wrap=�5�   $   $	��Top=�5�   $   ��5�   $   �� $ PopUpStringText�$ TwoWay=�$ PropertyChanged=�5�      5�      5�      5�      � 5�      -   5        $��progBar��5�      $��3��5�      $��4��5�      $��30��5�      $��5q�5�       � Minimum$ 0Q�5�       � Maximum$	 100Q�5�      $
��Bold"�5�        ProgressBarStyle#��[ 5�       5�      �� *	ProBarTag+ � Value 5�      �� *ProBarValue+5�      �� 5�      $��4��6   $��3��6&   .+�5�      � -   5        $��compoundsVersions��5�       5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(    5�      �� 5�      $��* �6+   �� 5�      $��* �6+   �� 5�      $��* �6+   .+�5�      �� $��Compund Name��6   $��0��63   $��0��6@   �� 5�      $��Current Version��6   $��0��66   $��1��6C   �� 5�      $��Latest Version��6   $��0��65   $��2��6B   �� 5�      $	��MCC��6    $��1��5�       y�5�       �� $ mccChkBx��$ 	IsChecked� �� 	Converter# [ �� 5�      $��MCC Language Pack��6    $��2��5�       y�5�       �� $ 
mccLpChkBx��$ 	IsChecked�# [ �� 5�      $��SysAdmin��6    $��3��5�       y�5�       �� $ sysAdminChkBx��$ 	IsChecked�# [ �� 5�      $��SysAdmin Language Pack��6    $��4��5�       y�5�       �� $ sysAdminLpChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��MCCCurrentVersion��5�       $��1��5�       $��1��5�       ��5�       �� *MCCCurrentVersionText+y�5�       �� $ mccChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��MCCLPCurrentVersion��5�       $��MCC Language Pack��5�       $��2��5�       $��1��5�       y�5�       �� $ 
mccLpChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��SysAdminCurrentVersion��5�       $��SysAdmin��5�       $��3��5�       $��1��5�       y�5�       �� $ sysAdminChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��SysAdminLPCurrentVersion��5�       $��SysAdmin Language Pack��5�       $��4��5�       $��1��5�       y�5�       �� $ sysAdminLpChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��MCCLatestVersion��5�       $��1��5�       $��2��5�       ��5�       �� *MCCLatestVersionText+y�5�       �� $ mccChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��MCCLPLatestVersion��5�       $��MCC Language Pack��5�       $��2��5�       $��2��5�       y�5�       �� $ 
mccLpChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��SysAdminLatestVersion��5�       $��SysAdmin��5�       $��3��5�       $��2��5�       y�5�       �� $ sysAdminChkBx��$ 	IsChecked�# [ �� 5�      -   5        $��SysAdminLPLatestVersion��5�       $��SysAdmin Language Pack��5�       $��4��5       $��2��5      y�5      �� $ sysAdminLpChkBx��$ 	IsChecked�# [ 5     5     5     5     5     