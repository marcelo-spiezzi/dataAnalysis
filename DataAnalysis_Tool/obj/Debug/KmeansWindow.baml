   M S B A M L   `   `   `  ����  DataAnalysis_Tool$    DataAnalysis_Tool.KmeansWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  KmeansWindow��5   	   $��550.4��6   $��1196.092��6-   .��5      � $��0,0,0.2,-1.6q�6   ��5   
   �� 5       �� Color# � ��6   .+�5	   
   �� -   5         ��Name$ tbKValue��5	      $
��Left=�6$   $��23��6?   $��252,238,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $��3��6t   $	��Top=�6}   $��51��6�   �� 5
   
   -   5        $��Import��5
      $
��Left=�6"   $��
580,26,0,0q�6=   $	��Top=�6Q   $��75��6i   6t   �� 5   
   -   5         ��Name$ BrowseInput��5      $��Browse��6&   $
��Left=�67   $��
490,26,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $ filePath��5      $
��Left=�6$   $��23��6?   $��	10,26,0,0q�6K   $
 Wrap=�6^   $)��#D:\Classes Assignemtns\Thesis\Data\��6r   $	��Top=�6�   $	��449��6�   �� 5   
   $
��Left=�6   $��	10,10,0,0q�6/   $
��Wrap=�6B   $��
File path:��6V   $	��Top=�6h   �� 5   
   $
��Left=�6   $��202,242,0,0q�6/   $
��Wrap=�6D   $��K value:��6X   $	��Top=�6h   $��45��6�   $��16��6�    J� IsHitTestVisible .   J� RenderTransformOrigin$ 0.649,0.409&�6�   �� 5   
   -   5         ��Name$ UserMessage��5      $��0,26,21.4,0q�6)   $
��Wrap=�6>   $	��Top=�6R   $��22��6j   $
��Bold"�6v    �� TextAlignment$ Right=�6�   $��Right=�6�   $ 0.5,0.5&�6�   $	��404��6�   �� 5   
   -   5        $ tbDimensions��5      $
��Left=�6(   $��23��6C   $��253,199,0,0q�6O   $
 Wrap=�6d   $��3��6x   $	��Top=�6�   $��51��6�   {�.  6�   �� 5   
   $
��Left=�6   $��183,202,0,0q�6/   $
��Wrap=�6D   $��Dimensions:��6X   $	��Top=�6k   $��69��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5   
   -   5        	 ��Name$	 
valuesList��5      $��24,81,0,94.8q�6&   $
��Left=�6<   $	��151��6W   �� 5   
   -   5        $	 analyzedList��5      $��0,81,700.4,95.8q�6(   $��Right=�6A   $ 0.5,0.5&�6]   $	��151��6}   �� 5   
   -	   5        $ AnalyzeButton��5      $��Analyze��6(   $
��Left=�6:   $��196,330,0,0q�6U   $	��Top=�6j   $��95��6�   {�.  6�   �� 5   
   -
   5        $ RegularGraph��5      $��
Show Graph��6'   $
��Left=�6<   $��24,0,0,62.8q�6W   $��Bottom=�6l   $	��151��6�   {�.  6�   �� 5   
   -   5        $ ClusterGraph��5      $��
Show Graph��6'   $��Right=�6<   $��0,0,703.4,62.8q�6X   $��Bottom=�6p   $	��151��6�   {�.  6�   �� 5   
   -   5        $ 	tbRndSeed��5      $
��Left=�6%   $��23��6@   $��252,277,0,0q�6L   $
 Wrap=�6a   $��3��6u   $	��Top=�6~   $��51��6�   �� 5   
   $
��Left=�6   $��184,280,0,0q�6/   $
��Wrap=�6D   $��	Rnd Seed:��6X   $	��Top=�6i   $��63��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5   
   -   5        $ SaveOutputButton��5      $��Save Output��6+   $
��Left=�6A   $��
580,0,0,15q�6\   $��Bottom=�6p   $��75��6�   {�.  6�   �� 5   
   -   5        $ BrowseOutput��5      $��Browse��6'   $
��Left=�68   $��
494,0,0,15q�6S   $��Bottom=�6g   $��75��6�   6�   �� 5   
   -   5        $ OutputFilePath��5      $
��Left=�6*   $��23��6E   $��	10,0,0,15q�6Q   $
 Wrap=�6d   $)��#D:\Classes Assignemtns\Thesis\Data\��6x   $��Bottom=�6�   $	��449��6�   �� 5   
   $
��Left=�6   $��10,0,0,37.8q�6/   $
��Wrap=�6D   $��Output File path:��6X   $��Bottom=�6q   �� 5   
   -   5        $	 	meansList��5      $��0,0,207.4,95q�6%   $��Right=�6;   $ 0.5,0.5&�6W   $	��316��6w   $��Bottom=�6�   $	��125��6�   �� 5   
   $
��Left=�6   $��666,0,0,221.4q�6/   $
��Wrap=�6F   $��Means:��6Z   $��Bottom=�6h   $��69��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5   
   -   5        $	 avgDistanceList��5      $��0,81,534.4,94.8q�6+   $��Right=�6D   $ 0.5,0.5&�6`   $	��148��6�   �� 5    
   $
��Left=�6   $��
507,59,0,0q�6/   $
��Wrap=�6C   $��Avg distance to centroid:��6W   $	��Top=�6x   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5!   
   -   5        $	 normalizedValuesList��5!      $��0,82,43.4,95q�60   $��Right=�6F   $ 0.5,0.5&�6b   $	��138��6�   �� 5"   
   $
��Left=�6   $��1007,59,0,0q�6/   $
��Wrap=�6D   $��Normalized Values��6X   $	��Top=�6q   $	��138��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5#   
   $
��Left=�6   $��
338,59,0,0q�6/   $
��Wrap=�6C   $��Clustered Values��6W   $	��Top=�6o   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5$   
   $
��Left=�6   $��	27,59,0,0q�6/   $
��Wrap=�6B   $��Imported Values��6V   $	��Top=�6m   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5%   
   -   5        $	 avgDistClusterList��5%      $��0,82,375.4,306.8q�6.   $��Right=�6H   $ 0.5,0.5&�6d   $	��148��6�   �� 5&   
   $
��Left=�6   $��
666,59,0,0q�6/   $
��Wrap=�6C   $��Avg distance per cluster��6W   $	��Top=�6w   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5'   
   -   5        $ tbAvgDistance��5'      $��Right=�6)   $��23��6E   $��0,0,375.4,252.8q�6Q   $
 Wrap=�6j   $��Bottom=�6~   $	��148��6�   �� 5(   
   $��Right=�6   $��0,0,375.4,285q�60   $
��Wrap=�6G   $��Total Average Distance��6[   $��Bottom=�6y   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5)   
   -   5        $��Normalize Data��5)      $
��Left=�6,   $��205,166,0,0q�6G   $	��Top=�6\   
 d� 	IsChecked$

 TrueY�6�   �� 5*   
   -   5        $��Return��5*      $
��Left=�6"   $��
676,26,0,0q�6=   $	��Top=�6Q   $��75��6i   6t   �� 5+   
   -   5        $	 numTulpsPerClusterList��5+      $��0,82,207.4,307q�62   $��Right=�6J   $ 0.5,0.5&�6f   $	��148��6�   �� 5,   
   $
��Left=�6   $��
833,59,0,0q�6/   $
��Wrap=�6C   $��Num of Tulps per cluster��6W   $	��Top=�6w   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 5-   
   -   5        $ ButtonSaveClusterIDs��5-      $��Save cluster sessions ID��6/   $
��Left=�6R   $��
730,0,0,16q�6m   $��Bottom=�6�   $	��136��6�   $��23��6�   6�   �� 5.   
   -   5        $ tbClusterToSave��5.      $��Right=�6+   $��23��6G   $��0,0,250.4,44q�6S   $
 Wrap=�6i   $��Bottom=�6}   $��42��6�   �� 5/   
   $
��Left=�6   $��
734,0,0,47q�6/   $
��Wrap=�6C   $!��Cluster to save sessions ID��6W   $��Bottom=�6z   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 50   
   $
��Left=�6   $��
885,0,0,18q�6/   $
��Wrap=�6C   $7��1Uses the same output path. Remember to change it!��6W   $��Bottom=�6�   $	��294��6�   $��16��6�    .  $ 0.649,0.409&�6�   �� 51   
   -   5        $ tbMeansAvgDist��51      $��Right=�6*   $��23��6F   $��0,0,208.4,253q�6R   $
 Wrap=�6i   $��Bottom=�6}   $	��148��6�   �� 52   
   $��Right=�6   $��0,0,207.4,286q�60   $
��Wrap=�6G   $��Means Average Distance��6[   $��Bottom=�6y   $	��148��6�   $��16��6�    .  $ 0.649,0.409&�6�   54      55      