<?php
include 'src/LangtonAnt.php';

$langton = new LangtonAnt(11, 6, 6, 'o', 5);
$langton->executeSteps();
$test = $langton->getSteps();
 ?>
 <html>
 	<head>
 		<title>Langton Ant</title>
 		<link rel="stylesheet" href="css/style.css">
		 <script type="text/javascript">var data = '<?php echo $test ?>';</script>
 		<script type="text/javascript" src="js/grid.js"></script>
 	</head>
 	<body>
 		<table id="grid">
 		</table>
 	</body>
</html>