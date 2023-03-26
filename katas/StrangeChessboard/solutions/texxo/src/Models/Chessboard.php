<?php
declare(strict_types=1);
namespace App\Models;
use LogicException;

class Chessboard
{
    public function __construct(
        private readonly array $columnStretch = [],
        private readonly array $rowStretch = [],
    )
    {
        if (count($this->columnStretch) !== count($this->rowStretch)){
            throw new LogicException('Invalid board layout. A chessboard needs to contain the same amount of x- and y-axis fields!');
        }
    }

    /**
     * @return array
     */
    public function getColumnStretch(): array
    {
        return $this->columnStretch;
    }

    /**
     * @return array
     */
    public function getRowStretch(): array
    {
        return $this->rowStretch;
    }

}