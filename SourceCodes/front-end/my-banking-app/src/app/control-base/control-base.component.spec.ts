import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlBaseComponent } from './control-base.component';

describe('ControlBaseComponent', () => {
  let component: ControlBaseComponent;
  let fixture: ComponentFixture<ControlBaseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ControlBaseComponent]
    });
    fixture = TestBed.createComponent(ControlBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
