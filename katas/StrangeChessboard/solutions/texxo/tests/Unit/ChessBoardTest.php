<?php
declare(strict_types=1);

namespace App\Tests;

use App\Models\Chessboard;
use LogicException;
use PHPUnit\Framework\TestCase;

class ChessBoardTest extends TestCase
{

    public function testCreateChessBoard(): void
    {
        // arrange / expect
        $this->expectException(LogicException::class);

        // act
        new Chessboard(
            columnStretch: [1, 2, 3],
            rowStretch: [1, 2, 3, 4]
        );
    }
}