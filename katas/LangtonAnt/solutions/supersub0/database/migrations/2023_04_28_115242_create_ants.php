<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class() extends Migration {
    public function up(): void
    {
        Schema::create('ants', function (Blueprint $table) {
            $table->id();
            $table->integer('position_y');
            $table->integer('position_x');
            $table->char('direction', 1);
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('ants');
    }
};
