import { Module } from '@nestjs/common';

import { AppController } from './app.controller';
import { LangtonAntService } from './langton-ant.service';

@Module({
  imports: [],
  controllers: [AppController],
  providers: [LangtonAntService],
})
export class AppModule {}
