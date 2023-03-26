<?php
declare(strict_types=1);
namespace App;
require_once '/Users/k.bielefeld/development/CodingDojo/katas/StrangeChessboard/solutions/texxo/vendor/autoload.php';
use App\Models\Chessboard;
use App\Services\CalculationService;

$calculationService = new CalculationService();

$chessBoard = new Chessboard(
    columnStretch: [3, 1, 2, 7, 1],
    rowStretch: [1, 8, 4, 5, 2]
);

$totalAreaPerColor = $calculationService->calculateTotalAreaForDifferentColors(
    columnStretch: $chessBoard->getColumnStretch(),
    rowStretch: $chessBoard->getRowStretch(),
);

print_r($totalAreaPerColor);
