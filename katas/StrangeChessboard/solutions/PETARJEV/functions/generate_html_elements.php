<?php
/**
 * Es l�dt das HTML Template
 */
function init()
{
    require config('template_path') . '/template.php';
}

function calculateTheSum(array $cs, array $rs) {
    $cs_length = count($cs);
    $rs_length = count($rs);
    //Der erste Check - ob die L�ngen der Arrays gleich sind
    if($cs_length != $rs_length) {
        return "Die Arrays haben unterschiedliche L�nge";
    }
    else {
        $total_white_area = 0;
        $total_black_area = 0;
        for($i = 0;$i < $rs_length; $i++) {
            for($j = 0;$j < $cs_length; $j++) {
                //Wenn die Indexes beider Arrays gerade sind oder ungerade sind, wird das Produkt der Werte der Elemente beider Array dem Wert f�r $total_white_area hinzugef�gt
                if(($j % 2 == 0 && $i % 2 == 0) || ($j % 2 != 0 && $i % 2 != 0)) {
                    $total_white_area = $total_white_area + $cs[$j] * $rs[$i];
                }
                else {
                    $total_black_area = $total_black_area + $cs[$j] * $rs[$i];
                }
        }  
        }
        $tuple = array();
        array_push($tuple,$total_white_area,$total_black_area);
        return $tuple;
    }
}
?>